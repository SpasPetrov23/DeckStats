namespace DeckStats.API.Utils.ExceptionHandling;

public class AppException : Exception
{
    public AppException(string message) : base(message) { }

    public AppException(ErrorCodes code) : base(code.ToString()) { }
    
    public AppException(ErrorCodes code, List<string> args) : base(code.ToString())
    {
        Args = args;
    }

    public List<string> Args { get; set; } = new();
}
