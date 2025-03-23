namespace APBD03.Exception;

public class OverfillException : System.Exception
{
    public OverfillException(string message) : base(message) { }
}