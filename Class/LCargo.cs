namespace APBD03.Class;

public class LCargo(string name, bool isHazardous) : Cargo(name)
{
    public bool IsHazardous { get; set; } = isHazardous;
}