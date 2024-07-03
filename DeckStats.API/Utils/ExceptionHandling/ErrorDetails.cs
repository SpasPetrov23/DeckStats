namespace DeckStats.API.Utils.ExceptionHandling;

public class ErrorDetails
{
    public ErrorDetails(int internalStatusCode, string message, List<string> args = null)
    {
        InternalStatusCode = internalStatusCode;
        StatusDescription = internalStatusCode.ToString();
        Errors.Add(message, args);
    }

    public int InternalStatusCode { get; }
    public string StatusDescription { get; }
    public Dictionary<string, List<string>> Errors { get; } = new();
}
