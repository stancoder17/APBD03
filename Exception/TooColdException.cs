namespace APBD03.Exception;

public class TooColdException : System.Exception
{
    public TooColdException(string message) : base(message) { }
}