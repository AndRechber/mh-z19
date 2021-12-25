using System;

namespace MHZ19.Sensor
{
    /// <summary>
    /// Errors when accessing the sensor
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SensorException : Exception
    {
        internal SensorException(string reason, Exception innerException) : base(reason, innerException)
        {
        }

        internal SensorException(string reason) : base(reason)
        {
        }
    }
}