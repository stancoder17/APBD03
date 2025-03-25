using APBD03.Exception;
using APBD03.Interface;

namespace APBD03.Class;

public class LContainer(double height, double netWeight, double depth, double maxLoadCapacity) : Container(height, netWeight, depth, maxLoadCapacity), IHazardNotifier
{
    private static int _id = 1;
    public LCargo? LiquidCargo { get; set; }
    
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
        return "KON-L-" + Id++;
    }
    
    /// <summary>
    /// If cargo is hazardous it may be loaded up to 50% of container's max load capacity, 90% if it's not hazardous
    /// </summary>
    /// <param name="massToLoad"></param>
    protected override void ValidateSpecificLoadingConditions(double massToLoad)
    {
        if (LiquidCargo == null)
            throw new NoCargoException("Cannot load cargo that is null");
        
        if (LiquidCargo.IsHazardous && Mass + massToLoad > MaxLoadCapacity * 0.5
            || !LiquidCargo.IsHazardous && Mass + massToLoad > MaxLoadCapacity * 0.9)
            NotifyDanger();
    }
    
    protected override void ValidateSpecificUnloadingConditions(double massToUnload)
    {
        if (LiquidCargo == null)
            throw new NoCargoException("Cannot unload cargo that is null");
    }
    
    public void NotifyDanger()
    {
        Console.WriteLine("DANGER!: " + SerialNumber);
    }
}