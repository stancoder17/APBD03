using APBD03.Cargos;
namespace APBD03.Containers;

public class CContainer : Container<CooledCargo>
{
    private static int _id = 1;
    public double Temperature { get; set; }

    /// <summary>
    /// Id: only get or increment by 1
    /// </summary>
    public int Id {
        get => _id;
        private set
        {
            if (value != _id + 1)
            {
                throw new ArgumentException("Id may only be incremented by 1");
            }
        } 
    }
    

    public CContainer(double mass, double height, double netWeight, double depth, double maxLoadCapacity, double temperature)
        : base(mass, height, netWeight, depth, maxLoadCapacity)
    {
        Temperature = temperature;
    }
    
    protected override string GenerateSerialNumber()
    {
        return "KON-C-" + _id++;
    }
    
    protected override void ValidateSpecificLoadingConditions(double massToLoad)
    {
        if (Temperature < Cargo.TemperatureRequired)
            throw new ArgumentException("Container's temperature must not be lower than cargo's required temperature");
    }
    
    protected override void ValidateSpecificUnloadingConditions(double massToUnload)
    {
        
    }
}