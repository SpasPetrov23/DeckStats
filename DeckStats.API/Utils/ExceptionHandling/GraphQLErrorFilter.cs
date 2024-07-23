using Microsoft.EntityFrameworkCore;
using ErrorCodes = DeckStats.Common.Errors.ErrorCodes;

namespace DeckStats.API.Utils.ExceptionHandling;

public class GraphQLErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        Console.WriteLine(error.Message);
        Console.WriteLine(error.Path);
        Console.WriteLine(error.Exception?.ToString() ?? error.Extensions?.ToJson());
        
        if (error.Exception is AppException appException)
        {
            return error
                .WithMessage(appException.Message)
                .SetExtension("args", appException.Args)
                .WithCode(appException.Message);
        }

        if (error.Exception is DbUpdateException db)
        {
            return error
                .WithException(db.InnerException)
                .WithMessage(db.InnerException?.Message ?? ErrorCodes.INTERNAL_SERVER_ERROR.ToString())
                .WithCode(ErrorCodes.INTERNAL_SERVER_ERROR.ToString());
        }

        return error
            .WithMessage(error.Message)
            .WithExtensions(error.Extensions ?? new ExtensionData())
            .WithCode(error.Code);
    }
}
