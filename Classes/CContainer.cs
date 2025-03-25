using APBD03.Exception;

namespace APBD03.Classes;

public class CContainer(double height, double netWeight, double depth, double maxLoadCapacity, double temperature) : Container(height, netWeight, depth, maxLoadCapacity)
{
    private static int _id = 1;
    private double _temperature = temperature; // celsius
    public CCargo? CooledCargo { get; set; }

    /// <summary>
    /// Id: only get or increment by 1
    /// </summary>
    public int Id {
        get => _id;
        private set
        {
            if (value != _id + 1)
                throw new ArgumentException("Id may only be incremented by 1");
            
            _id = value;
        } 
    }

    public double Temperature
    {
        get => _temperature;
        set
        {
            if (value < -273.15)
                throw new ArgumentException("Temperature cannot be lower than -273.15 degrees Celsius");
        }
    }
    
    protected override string GenerateSerialNumber()
    {
        return "KON-C-" + _id++;
    }
    
    protected override void ValidateSpecificLoadingConditions(double massToLoad)
    {
        if (CooledCargo == null)
            throw new NoCargoException("Cannot load cargo that is null");
        
        if (Temperature < CooledCargo.TemperatureRequired)
            throw new TooColdException("Container's temperature must not be lower than cargo's required temperature");
    }
    
    protected override void ValidateSpecificUnloadingConditions(double massToUnload)
    {
        if (CooledCargo == null)
            throw new NoCargoException("Cannot unload cargo that is null");
    }

    public override string ToString()
    {
        return base.ToString() + $", temperature: {Temperature} C";
    }
}