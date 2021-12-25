using System;
using MHZ19.Sensor;

namespace MHZ19.TestTool
{
    internal class Program
    {
        private static string DEFAULT_SERIAL_PORT = "/dev/serial0";

        private static void Main(string[] args)
        {
            Console.WriteLine("MH-Z19 Sensor Test Tool");
            Console.WriteLine("");
            Console.WriteLine("Serial Port name or default " + DEFAULT_SERIAL_PORT + " when empty");
            string serialPort = Console.ReadLine();
            if (string.IsNullOrEmpty(serialPort))
            {
                serialPort = DEFAULT_SERIAL_PORT;
            }
            Console.WriteLine("");
            Console.WriteLine("Read data from " + serialPort);
            try
            {
                using (MHZ19Sensor sensor = SensorFactory.CreateUartSensor(serialPort))
                {
                    sensor.StartZeroPointCalibration();
                    //Measurement measurement = sensor.Read();
                    //Console.WriteLine("Co2: " + measurement.Co2 + " ppm");
                    //Console.WriteLine("Temperature: " + measurement.Temperature + " °C");
                }
            }
            catch (SensorException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}