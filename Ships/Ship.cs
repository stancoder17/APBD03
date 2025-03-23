using APBD03.Cargos;
using APBD03.Containers;

namespace APBD03.Ships;

public class Ship
{
    public List<Container<Cargo>>? Containers { get; }
    public int MaxSpeed { get; set; } // knots
    public int MaxNumOfContainers { get; set; }
    public double MaxContainersMass { get; set; }

    public Ship(int maxSpeed, int maxNumOfContainers, double maxContainersMass)
    {
        Containers = new List<Container<Cargo>>();
        MaxSpeed = maxSpeed;
        MaxNumOfContainers = maxNumOfContainers;
        MaxContainersMass = maxContainersMass;
    }

    public void AddContainer(Container<Cargo> container)
    {
        Containers.Add(container);
    }
}