namespace MHZ19.Sensor
{
    /// <summary>
    /// Measurement result of sensor
    /// </summary>
    public sealed class Measurement
    {
        /// <summary>
        /// CO2 Value in ppm
        /// </summary>
        /// <value>
        /// The co2.
        /// </value>
        public int Co2 { get; private set; }

        /// <summary>
        /// Temperature in Celsius
        /// </summary>
        /// <value>
        /// The temperature.
        /// </value>
        public int Temperature { get; private set; }

        internal Measurement(int co2ppm, int temp)
        {
            this.Co2 = co2ppm;
            this.Temperature = temp;
        }
    }
}