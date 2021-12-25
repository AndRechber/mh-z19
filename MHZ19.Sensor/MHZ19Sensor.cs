using System;

namespace MHZ19.Sensor
{
    /// <summary>
    /// Facade to access MH-Z19 Sensor
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface MHZ19Sensor : IDisposable
    {
        /// <summary>
        /// Read data from sensor
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SensorException">Serialport or message error</exception>
        Measurement Read();

        /// <summary>
        /// Enable Auto Base Calibration (Device per default enabled)
        /// The self-calibration function means that after the sensor runs continuously for a period of time, it can
        /// intelligently determine the zero point according to the environmental concentration and calibrate itself. The
        /// calibration cycle is automatic calibration every 24 hours since power-on operation. The zero point of automatic
        /// calibration is 400ppm.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        /// <exception cref="SensorException">Serialport error</exception>
        void EnableAutoBaseCalibration(bool enable);

        /// <summary>
        /// Start manually the Zero Point Calibration.
        /// It is essential that, during the manual calibration process, the sensor is running beforehand for,
        /// at least 20 minutes, in a stable CO2 environment with a fresh air CO2 concentration of 400ppm
        /// (outdoors or by a window, for example).
        /// </summary>
        /// <exception cref="SensorException">Serialport error</exception>
        void StartZeroPointCalibration();
    }
}