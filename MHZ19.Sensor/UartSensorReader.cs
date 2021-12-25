using System;
using System.IO.Ports;
using System.Threading;

namespace MHZ19.Sensor
{
    /// <summary>
    /// MHZ19Sensor implementation via UART
    /// </summary>
    /// <seealso cref="MHZ19.Sensor.MHZ19Sensor" />
    internal class UartSensorReader : MHZ19Sensor
    {
        private static readonly byte[] READ_VALUES_CMD = { 0xff, 0x01, 0x86, 0x00, 0x00, 0x00, 0x00, 0x00, 0x79 };
        private static readonly byte[] ENABLE_AUTO_CALIBRATION = { 0xff, 0x01, 0x79, 0xa0, 0x00, 0x00, 0x00, 0x00, 0xe6 };
        private static readonly byte[] DISABLE_AUTO_CALIBRATION = { 0xff, 0x01, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x86 };
        private static readonly byte[] MANUAL_ZERO_POUNT_CALIBRATION = { 0xff, 0x01, 0x87, 0x00, 0x00, 0x00, 0x00, 0x00, 0x78 };

        private SerialPort serialPort;

        internal UartSensorReader(SerialPort serialPort)
        {
            this.serialPort = serialPort;
        }

        public Measurement Read()
        {
            try
            {
                openSerialPort();
                writeToSerialPort(READ_VALUES_CMD);
                byte[] result = readFromSerialPort();
                verifyMeasurementResult(result);
                int co2Ppm = result[2] * 256 + result[3];
                int temperature = result[4] - 40;
                return new Measurement(co2Ppm, temperature);
            }
            catch (SensorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SensorException("Read measurement error. " + ex.Message, ex);
            }
        }

        public void EnableAutoBaseCalibration(bool enable)
        {
            openSerialPort();
            if (enable)
            {
                writeToSerialPort(ENABLE_AUTO_CALIBRATION);
            }
            else
            {
                writeToSerialPort(DISABLE_AUTO_CALIBRATION);
            }
        }

        public void StartZeroPointCalibration()
        {
            openSerialPort();
            writeToSerialPort(MANUAL_ZERO_POUNT_CALIBRATION);
        }

        public void Dispose()
        {
            this.serialPort.Close();
            this.serialPort.Dispose();
        }

        private void writeToSerialPort(byte[] command)
        {
            try
            {
                serialPort.Write(command, 0, 9);
            }
            catch (Exception ex)
            {
                throw new SensorException("Write to serial port error.", ex);
            }
        }

        private byte[] readFromSerialPort()
        {
            waitForResponse();
            byte[] buffer = new byte[9];
            serialPort.Read(buffer, 0, 9);
            verifyChecksum(buffer);
            return buffer;
        }

        private void openSerialPort()
        {
            try
            {
                if (!this.serialPort.IsOpen)
                {
                    this.serialPort.Open();
                }
            }
            catch (Exception ex)
            {
                throw new SensorException("Could not open serial port", ex);
            }
        }

        private void waitForResponse()
        {
            for (int i = 0; i < 5; i++)
            {
                if (serialPort.BytesToRead >= 9)
                {
                    return;
                }
                Thread.Sleep(100);
            }
            throw new SensorException("No response received");
        }

        private static void verifyChecksum(byte[] buffer)
        {
            byte crc = calculateChecksum(buffer);
            if (buffer[8] != crc)
            {
                throw new SensorException("Ready measurements. Invalid checksum received");
            }
        }

        private static byte calculateChecksum(byte[] buffer)
        {
            byte crc = 0;
            for (var i = 1; i < 8; i++)
            {
                crc += buffer[i];
            }
            crc = (byte)(0xff ^ crc);
            crc += 1;
            return crc;
        }

        private static void verifyMeasurementResult(byte[] result)
        {
            if (result[0] == 0xff && result[1] == 0x86)
            {
                return;
            }
            string hexResult = BitConverter.ToString(result);
            throw new SensorException("Unexpcted result received: " + hexResult);
        }
    }
}