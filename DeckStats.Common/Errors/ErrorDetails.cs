namespace DeckStats.Common.Errors;

public class ErrorDetails
{
    public ErrorDetails(int httpStatusCode, string message, List<string> args = null)
    {
        HttpStatusCode = httpStatusCode;
        Message = message;
        Args = args;
    }

    public int HttpStatusCode { get; }
    public string Message { get; }
    public List<string> Args { get; }
}
