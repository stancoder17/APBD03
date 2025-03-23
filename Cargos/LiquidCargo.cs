namespace APBD03.Cargos;

public class LiquidCargo : Cargo
{
    public bool IsHazardous { get; }

    public LiquidCargo(string name, bool isHazardous) : base(name)
    {
        IsHazardous = isHazardous;
    }
}