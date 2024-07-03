namespace DeckStats.API.Utils.ExceptionHandling;

public class GraphQLErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        Console.WriteLine(error.Message);
        Console.WriteLine(error.Exception);
        
        if (error.Exception is AppException appException)
        {
            return error
                .WithMessage(appException.Message)
                .SetExtension("args", appException.Args)
                .WithCode(appException.Message);
        }

        if (error.Exception != null)
        {
            Console.WriteLine(error.Exception.InnerException?.Message ?? ErrorCodes.INTERNAL_SERVER_ERROR.ToString());
        
            return error
                .WithException(error.Exception.InnerException)
                .WithMessage(error.Exception.InnerException?.Message ?? ErrorCodes.INTERNAL_SERVER_ERROR.ToString())
                .WithCode(ErrorCodes.INTERNAL_SERVER_ERROR.ToString());
        }

        //TODO: DbException?

        return error
            .WithMessage(error.Message)
            .WithExtensions(error.Extensions ?? new ExtensionData())
            .WithCode(error.Code);
    }
}
