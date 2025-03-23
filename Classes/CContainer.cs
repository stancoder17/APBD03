namespace APBD03.Classes;

public class CContainer(double height, double netWeight, double depth, double maxLoadCapacity, double temperature) : Container(height, netWeight, depth, maxLoadCapacity)
{
    private static int _id = 1;
    public double Temperature { get; set; } = temperature;
    public CooledCargo CooledCargo { get; set; }

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
    
    protected override string GenerateSerialNumber()
    {
        return "KON-C-" + _id++;
    }
    
    protected override void ValidateSpecificLoadingConditions(double massToLoad)
    {
        if (Temperature < CooledCargo.TemperatureRequired)
            throw new ArgumentException("Container's temperature must not be lower than cargo's required temperature");
    }
    
    protected override void ValidateSpecificUnloadingConditions(double massToUnload)
    {
        
    }

    public override string ToString()
    {
        return base.ToString() + $", temperature: {Temperature}";
    }
}