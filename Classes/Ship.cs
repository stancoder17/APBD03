namespace APBD03.Classes;

public class Ship(string name, double maxSpeed, double maxNumOfContainers, double maxAllContainersWeight)
{
    private List<Container> Containers { get; set; } = [];
    private string _name = name;
    private double _maxSpeed = maxSpeed; // knots
    private double _maxNumOfContainers = maxNumOfContainers; // tons
    private double _maxAllContainersWeight = maxAllContainersWeight;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Name cannot be empty");
            
            _name = value;
        }
    }

    public double MaxSpeed
    {
        get => _maxSpeed;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Maximum speed must be positive");
            
            _maxSpeed = value;
        }
    }

    public double MaxNumOfContainers
    {
        get => _maxNumOfContainers;
        set
        {
            if (value < 0)
                throw new ArgumentException("Maximum number of containers must not be negative");
            
            _maxNumOfContainers = value;
        }
    }

    public double MaxAllContainersWeight
    {
        get => _maxAllContainersWeight;
        set
        {
            if (value < 0)
                throw new ArgumentException("Maximum all containers weight must not be negative");
            
            _maxAllContainersWeight = value;
        }
    }

    public void AddContainer(Container containerToAdd)
    {
        if (IsAddingPossible(containerToAdd))
        {
            Containers.Add(containerToAdd);
            Console.WriteLine($"Added container {containerToAdd.SerialNumber} to {Name}");
        }
        else
            throw new ApplicationException($"Cannot add container {containerToAdd.SerialNumber} to {Name}");
    }

    public void AddManyContainers(List<Container> containersToAdd)
    {
        if (IsAddingPossible(containersToAdd))
        {
            foreach (var container in containersToAdd)
                AddContainer(container);
        } 
        else 
            throw new ApplicationException($"Cannot add containers to {Name}");
        
    }

    public void RemoveContainer(Container containerToRemove)
    {
        if (IsRemovingPossible(containerToRemove))
            Containers.Remove(containerToRemove);
        else
            throw new ApplicationException($"Cannot remove container {containerToRemove.SerialNumber} from {Name}");
        
    }

    public void SwapContainers(Container toAdd, Container toRemove)
    {
        if (!IsRemovingPossible(toRemove))
            throw new ApplicationException($"Swapping operation aborted - cannot remove container {toRemove.SerialNumber} from {Name}");
        
        if (Containers.Contains(toAdd) && Containers.Contains(toRemove))
            throw new ApplicationException($"Swapping operation aborted - both containers [{toAdd.SerialNumber} and {toRemove.SerialNumber}] are on {Name}");
        
        // Check if adding is possible BEFORE removing a container
        Ship temp = new Ship(Name, MaxSpeed, MaxNumOfContainers, MaxAllContainersWeight);
        temp.Containers = Containers.ToList();
        temp.RemoveContainer(toRemove);

        if (temp.IsAddingPossible(toAdd))
        {
            RemoveContainer(toRemove);
            AddContainer(toAdd);
        }
        else 
            throw new ApplicationException($"Swapping operation aborted - cannot add container {toAdd.SerialNumber} to {Name}");
    }

    
    // Example method with string instead of Container
    public void SwapContainers(string containerToAddSerialNumber, Container containerToRemove)
    {
        SwapContainers(containerToAddSerialNumber, Containers.FirstOrDefault(c => c.SerialNumber == containerToAddSerialNumber) ?? 
                              throw new InvalidOperationException($"The container with serial number \"{containerToAddSerialNumber}\" was not found"));
    }

    public void TransportContainerToAnotherShip(Container containerToTransport, Ship targetShip)
    {
        if (targetShip.IsAddingPossible(containerToTransport))
        {
            RemoveContainer(containerToTransport);
            targetShip.AddContainer(containerToTransport);
            Console.WriteLine($"Container {containerToTransport.SerialNumber} transported to {targetShip.Name}");
        }
        else
            throw new ApplicationException($"Cannot transport container {containerToTransport.SerialNumber} to {targetShip.Name}");
    }

    public bool IsAddingPossible(Container containerToAdd)
    {
        if (Containers.Contains(containerToAdd))
            return false;
        if (Containers.Count + 1 > MaxNumOfContainers)
            return false;
        if (CalculateTotalCargoMass(Containers) + containerToAdd.Mass > MaxAllContainersWeight)
            return false;
        
        return true;
    }

    public bool IsAddingPossible(List<Container> containersToAdd)
    {
        if (Containers.Count + containersToAdd.Count > MaxNumOfContainers)
            return false;
        foreach (var container in containersToAdd)
        {
            if (Containers.Contains(container))
                return false;
        }
        
        if (CalculateTotalCargoMass(Containers) + CalculateTotalCargoMass(containersToAdd) > MaxAllContainersWeight)
            return false;
        
        return true;
    }

    public bool IsRemovingPossible(Container containerToRemove)
    {
        return Containers.Contains(containerToRemove);
    }

    public void DisplayContainerInfo(Container container)
    {
        Console.WriteLine(container.ToString());
    }

    public override string ToString()
    {
        return $"[Ship {Name}] -- maximum speed: {MaxSpeed} kts, maximum number of containers: {MaxNumOfContainers}, maximum all containers weight: {MaxAllContainersWeight} kg, total cargo mass: {CalculateTotalCargoMass(Containers)} kg";
    }

    public int GetNumberOfContainers()
    {
        return Containers.Count;
    }

    public double CalculateTotalCargoMass(List<Container> containers)
    {
        double totalCargoMass = 0;
        
        foreach (var container in containers)
            totalCargoMass += container.Mass;
        
        return totalCargoMass;
    }
}