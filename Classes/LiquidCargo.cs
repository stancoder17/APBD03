namespace APBD03.Classes;

public class LiquidCargo(string name, bool isHazardous) : Cargo(name)
{
    public bool IsHazardous { get; set; } = isHazardous;
}