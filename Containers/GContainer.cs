using APBD03.Cargos;
using Containers;
namespace APBD03.Containers;

public class GContainer : Container<GasCargo>, IHazardNotifier
{
    private static int _id = 1;
    private double _pressure; // atm

    /// <summary>
    /// Id: only get or increment by 1
    /// </summary>
    public int Id {
        get => _id;
        private set
        {
            if (value != _id + 1)
                throw new ArgumentException("Id may only be incremented by 1");
        } 
    }

    /// <summary>
    /// Make sure that pressure is not negative
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public double Pressure
    {
        get => _pressure;
        set
        {
            if (value < 0 || _pressure - value < 0)
                throw new ArgumentException("Pressure cannot be negative");
        }
    }


    public GContainer(double mass, double height, double netWeight, double depth, double maxLoadCapacity, double pressure)
        : base(mass, height, netWeight, depth, maxLoadCapacity)
    {
        Pressure = pressure;
    }
    
    protected override string GenerateSerialNumber()
    {
        return "KON-G-" + Id++;
    }
    
    protected override void ValidateSpecificLoadingConditions(double massToLoad)
    {
        
    }
    
    
    /// <summary>
    /// While unloading the gas cargo, 5% of it's load must be left inside the container
    /// </summary>
    /// <param name="massToUnload"></param>
    protected override void ValidateSpecificUnloadingConditions(double massToUnload)
    {
        if (Mass - massToUnload < Mass * 0.05)
            NotifyDanger();
    }

    public void NotifyDanger()
    {
        Console.WriteLine("!! DANGER !!!: " + SerialNumber);
    }
}