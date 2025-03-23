using APBD03.Cargos;
namespace APBD03.Containers;

public class LContainer : Container<LiquidCargo>, IHazardNotifier
{
    private static int _id = 1;
    
    /// <summary>
    /// Id: only get or increment by 1
    /// </summary>
    public int Id {
        get
        {
            return _id;
        }
        private set
        {
            if (value != _id + 1)
            {
                throw new ArgumentException("Id may only be incremented by 1");
            }
        } 
    }
    
    

    public LContainer(double mass, double height, double netWeight, double depth, double maxLoadCapacity)
        : base(mass, height, netWeight, depth, maxLoadCapacity) { }

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
        if (Cargo.IsHazardous && Mass + massToLoad > MaxLoadCapacity * 0.5 || !Cargo.IsHazardous && Mass + massToLoad > MaxLoadCapacity * 0.9)
            NotifyDanger();
    }
    
    protected override void ValidateSpecificUnloadingConditions(double massToUnload)
    {
        
    }

    
    public void NotifyDanger()
    {
        Console.WriteLine("DANGER!: " + SerialNumber);
    }
}