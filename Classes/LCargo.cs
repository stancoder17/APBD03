namespace APBD03.Classes;

public class LCargo(string name, bool isHazardous) : Cargo(name)
{
    public bool IsHazardous { get; set; } = isHazardous;
}