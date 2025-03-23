using APBD03.Cargos;
using Containers;
namespace APBD03.Containers;

public abstract class Container<TCargo> where TCargo : Cargo
{
    
    /// Fields
    private double _mass; // kg
    public double Height { get; } // cm
    public double NetWeight { get; } // kg
    public double Depth { get; } // cm
    private double _maxLoadCapacity; // kg
    public string SerialNumber { get; }
    
    /// <summary>
    /// There may only be one type of cargo in the container
    /// </summary>
    public TCargo Cargo { get; set; }
    
    
    /// Constructor 
    protected Container(double mass, double height, double netWeight, double depth, double maxLoadCapacity)
    {
        Mass = mass;
        Height = height;
        NetWeight = netWeight;
        Depth = depth;
        MaxLoadCapacity = maxLoadCapacity;
        SerialNumber = GenerateSerialNumber();
    }
    

    
    /// Getters and setters 
    public double Mass
    {
        get => _mass;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(Mass), "Masa nie może być ujemna");
            
            _mass = value;
        } 
    }
    

    public double MaxLoadCapacity
    {
        get => _maxLoadCapacity;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(MaxLoadCapacity),
                    "Maksymalna ładowność nie może być ujemna");
            
            _maxLoadCapacity = value;
        }
    }


    /// Methods
    public void LoadCargo(double massToLoad)
    {
        // Check general conditions
        if (massToLoad < 0)
            throw new ArgumentOutOfRangeException(nameof(massToLoad), "Mass to load must not be negative");
        
        if (Mass + massToLoad > MaxLoadCapacity)
            throw new OverfillException("Too large mass to load");
        
        
        // Check conditions that apply to a specific Container type
        ValidateSpecificLoadingConditions(massToLoad);
        
        
        // Loading cargo
        Mass += massToLoad;
    }

    
    public void UnloadCargo(double massToUnload)
    {
        // Check general conditions
        if (massToUnload < 0)
            throw new ArgumentOutOfRangeException(nameof(massToUnload), "Mass to load must not be negative");
        
        
        // Check conditions that apply to a specific Container type
        ValidateSpecificUnloadingConditions(massToUnload);
        
        
        // Unloading cargo
        if (massToUnload > Mass)
            Mass = 0;
        else 
            Mass -= massToUnload;
    }


    /// Abstract methods 
    protected abstract string GenerateSerialNumber();

    protected abstract void ValidateSpecificLoadingConditions(double massToLoad);
    
    protected abstract void ValidateSpecificUnloadingConditions(double massToUnLoad);
}