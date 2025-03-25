namespace APBD03.Class;

public class Cargo(string name)
{
    private string _name = name;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Name cannot be empty");
        }
    }
}
