﻿namespace APBD03.Cargos;

public class CooledCargo : Cargo
{
    public double TemperatureRequired { get; }

    public CooledCargo(string name, double temperatureRequired) : base(name)
    {
        TemperatureRequired = temperatureRequired;
    }
}