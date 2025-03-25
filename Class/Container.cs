using APBD03.Exception;

namespace APBD03.Class;

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
                throw new ArgumentException("Mass cannot be negative");
            
            _mass = value;
        } 
    }

    public double Height
    {
        get => _height;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Height must be positive");
            
            if (value < _depth)
                throw new ArgumentException("Height cannot be lower than depth");
            
            _height = value;
        }
    }

    public double NetWeight
    {
        get => _netWeight;
        set 
        {
            if (value < 0)
                throw new ArgumentException("Net weight cannot be negative");
            
            _netWeight = value;
        }
    }

    public double Depth
    {
        get => _depth;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Depth must be positive");
            
            _depth = value;
        }
    }
    public double MaxLoadCapacity
    {
        get => _maxLoadCapacity;
        set
        {
            if (value < 0)
                throw new ArgumentException("Max load capacity cannot be negative");
            
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
        Console.WriteLine($"Loading {massToLoad} kg of cargo");
    }
    
    public void UnloadCargo(double massToUnload)
    {
        // Check general conditions
        if (massToUnload < 0)
            throw new ArgumentException("Mass to load must not be negative");
        
        
        // Check conditions that apply to a specific Container type
        ValidateSpecificUnloadingConditions(massToUnload);
        
        
        // Unloading cargo
        if (massToUnload > Mass)
        {
            Mass = 0;
            Console.WriteLine("Selected mass to unload is too big, unloading all cargo");
        }
        else
        {
            Mass -= massToUnload;
            Console.WriteLine($"Unloading {massToUnload} kg of cargo");
        }
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
        return $"[Container {SerialNumber}] -- mass: {Mass} kg, height: {Height} cm, net weight: {NetWeight} kg, depth: {Depth} cm, max load capacity: {MaxLoadCapacity} kg";
    }
}

