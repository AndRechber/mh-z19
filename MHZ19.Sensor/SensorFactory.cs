using System.IO.Ports;

namespace MHZ19.Sensor
{
    /// <summary>
    /// Factory for creating Sensor
    /// </summary>
    public static class SensorFactory
    {
        /// <summary>
        /// Creator of Sensor
        /// </summary>
        /// <param name="serialPort">The serial port to access sensor. Example: For windows COM1, for linux /dev/serial0</param>
        /// <returns>MHZ19Sensor</returns>
        public static MHZ19Sensor CreateUartSensor(string serialPort)
        {
            SerialPort port = new SerialPort
            {
                BaudRate = 9600,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                PortName = serialPort
            };
            return new UartSensorReader(port);
        }
    }
}