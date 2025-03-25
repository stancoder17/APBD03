using APBD03.Exception;
using APBD03.Interface;

namespace APBD03.Classes;

public class GContainer(double height, double netWeight, double depth, double maxLoadCapacity, double pressure) : Container(height, netWeight, depth, maxLoadCapacity), IHazardNotifier
{
    private static int _id = 1;
    public double Pressure { get; set; } = pressure; // atm
    public GCargo? GasCargo { get; set; }

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
        return "KON-G-" + Id++;
    }
    
    protected override void ValidateSpecificLoadingConditions(double massToLoad)
    {
        if (GasCargo == null)
            throw new NoCargoException("Cannot load cargo that is null");
    }
    
    
    /// <summary>
    /// While unloading the gas cargo, 5% of it's load must be left inside the container
    /// </summary>
    /// <param name="massToUnload"></param>
    protected override void ValidateSpecificUnloadingConditions(double massToUnload)
    {
        if (GasCargo == null)
            throw new NoCargoException("Cannot unload cargo that is null");
        
        if (Mass - massToUnload < Mass * 0.05)
            NotifyDanger();
    }

    public void NotifyDanger()
    {
        Console.WriteLine("!! DANGER !!!: " + SerialNumber);
    }

    public override string ToString()
    {
        return base.ToString() + $", pressure: {Pressure} atm";
    }
}