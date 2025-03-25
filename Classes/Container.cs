using APBD03.Exception;

namespace APBD03.Classes;

public abstract class Container 
{
    
    /// Fields
    private double _mass; // kg
    private double _height; // cm
    private double _netWeight; // kg
    private double _depth; // cm
    private double _maxLoadCapacity; // kg
    public string SerialNumber { get; }
    
    
    /// Constructor 
    protected Container(double height, double netWeight, double depth, double maxLoadCapacity)
    {
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

    public double Height
    {
        get => _height;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Wysokość musi być dodatnia");
            
            _height = value;
        }
    }

    public double NetWeight
    {
        get => _netWeight;
        set 
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Masa własna nie może być ujemna");
            
            _netWeight = value;
        }
    }

    public double Depth
    {
        get => _depth;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Głębokość nie może być ujemna");
            
            _depth = value;
        }
    }


    /// Methods
    public double MaxLoadCapacity
    {
        get => _maxLoadCapacity;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(MaxLoadCapacity), "Maksymalna ładowność nie może być ujemna");
            
            _maxLoadCapacity = value;
        }
    }

    
    
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
        {
            Mass = 0;
            Console.WriteLine("Mass to unload is too big, unloading all cargo");
        }
        else 
            Mass -= massToUnload;
    }

    public double GetTotalWeight()
    {
        return Mass + NetWeight;
    }
    
    /// Abstract methods 
    protected abstract string GenerateSerialNumber();

    protected abstract void ValidateSpecificLoadingConditions(double massToLoad);
    
    protected abstract void ValidateSpecificUnloadingConditions(double massToUnLoad);

    public override string ToString()
    {
        return $"[Container {SerialNumber}] -- mass: {Mass}, height: {Height}, net weight: {NetWeight}, depth: {Depth}, max load capacity: {MaxLoadCapacity}";
    }
}

