# mh-z19
Library to access MH-Z19 CO2 Sensor written in .NET 5


## Library Usage
Library to read measuerement data and calibrate sensor.


Example:
Access via Raspberry Pi 
```csharp
   using (MHZ19Sensor sensor = SensorFactory.CreateUartSensor("/dev/serial0"))
   {
       Measurement measurement = sensor.Read();
       Console.WriteLine("Co2: " + measurement.Co2 + " ppm");
       Console.WriteLine("Temperature: " + measurement.Temperature + " °C");
   }
```


## MHZ19.TestTool
This is a console test tool for testing the library.
Start: sudo dotnet MHZ19.TestTool.dll 


## Tested
Tested with MH-Z19C Sensor (https://www.winsen-sensor.com/sensors/co2-sensor/mh-z19c.html) and Raspberry Pi 2
Documentation of sensor https://www.winsen-sensor.com/d/files/infrared-gas-sensor/mh-z19c-pins-type-co2-manual-ver1_0.pdf
