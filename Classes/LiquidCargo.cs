namespace APBD03.Classes;

public class LiquidCargo : Cargo
{
    public bool IsHazardous { get; }

    public LiquidCargo(string name, bool isHazardous) : base(name)
    {
        IsHazardous = isHazardous;
    }
}