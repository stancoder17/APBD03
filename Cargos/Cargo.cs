﻿namespace APBD03.Cargos;

public class Cargo
{
    public string Name { get; }

    protected Cargo(string name)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name));
        
        Name = name;
    }
}
