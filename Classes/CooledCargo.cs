namespace APBD03.Classes;

public class CooledCargo(string name, double temperatureRequired) : Cargo(name)
{
    private double _temperatureRequired = temperatureRequired;

    public double TemperatureRequired
    {
        get => _temperatureRequired;
        set
        {
            if (value < -273.15)
                throw new ArgumentException("Temperature cannot be lower than -273.15 degrees Celsius");
        }
    }
}