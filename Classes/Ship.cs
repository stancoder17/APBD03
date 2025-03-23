namespace APBD03.Classes;

public class Ship(string name, double maxSpeed, double maxNumOfContainers, double maxAllContainersWeight)
{
    private List<Container> Containers { get; set; } = [];
    public string Name { get; set; } = name;
    public double MaxSpeed { get; set; } = maxSpeed;
    public double MaxNumOfContainers { get; set; } = maxNumOfContainers;
    public double MaxAllContainersWeight { get; set; } = maxAllContainersWeight; // tons 

    public void AddContainer(Container containerToAdd)
    {
        if (IsAddingPossible(containerToAdd))
            Containers.Add(containerToAdd);
        else
            throw new ApplicationException("Cannot add container to the ship");
    }

    public void AddManyContainers(List<Container> containersToAdd)
    {
        if (IsAddingPossible(containersToAdd))
        {
            foreach (var container in containersToAdd)
                AddContainer(container);
        } 
        else 
            throw new ApplicationException("Cannot add containers to the ship");
        
    }

    public void RemoveContainer(Container container)
    {
        if (IsRemovingPossible(container))
            Containers.Remove(container);
        else
            throw new ApplicationException("Cannot remove container from the ship");
        
    }

    public void SwapContainers(Container toAdd, Container toRemove)
    {
        if (!IsRemovingPossible(toRemove))
            throw new ApplicationException("Cannot remove container from the ship");
        
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
            throw new ApplicationException("Cannot add container to the ship");
    }

    public void TransportContainerToAnotherShip(Container container, Ship targetShip)
    {
        if (targetShip.IsAddingPossible(container))
        {
            RemoveContainer(container);
            targetShip.AddContainer(container);
        }
        else
            throw new ApplicationException("Cannot transport container to another ship");
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
        if (!Containers.Contains(containerToRemove))
            return false;

        return true;
    }

    public void DisplayContainerInfo(Container container)
    {
        Console.WriteLine(container.ToString());
    }

    public override string ToString()
    {
        return $"[Ship {Name}] -- maximum speed: {MaxSpeed} kts, maximum number of containers: {MaxNumOfContainers}, maximum all containers weight: {MaxAllContainersWeight} kg, total cargo mass: {CalculateTotalCargoMass(Containers)} kg";
    }

    public double CalculateTotalCargoMass(List<Container> containers)
    {
        double totalCargoMass = 0;
        
        foreach (var container in containers)
            totalCargoMass += container.Mass;
        
        return totalCargoMass;
    }
}