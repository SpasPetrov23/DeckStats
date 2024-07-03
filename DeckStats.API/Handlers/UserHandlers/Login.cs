using DeckStats.API.Utils.Authentication;
using DeckStats.API.Utils.ExceptionHandling;
using DeckStats.Data;
using DeckStats.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ErrorCodes = DeckStats.API.Utils.ExceptionHandling.ErrorCodes;

namespace DeckStats.API.Handlers.UserHandlers;

public class Login
{
    public record Mutation : IRequest<Result>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public record Result(string Token);

    public class Handler : IRequestHandler<Mutation, Result>
    {
        private readonly TokenProvider _tokenProvider;
        private readonly DeckStatsDbContext _dbContext;

        public Handler(DeckStatsDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _tokenProvider = new(config, _dbContext);
        }

        public async Task<Result> Handle(Mutation request, CancellationToken cancellationToken)
        {
            User user = await Authenticate(request.Email, request.Password);
        
            string jwtToken = _tokenProvider.GenerateJwtToken(user);

            return new(jwtToken);
        }
        
        private async Task<User> Authenticate(string email, string password)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        
            if (user is null)
            {
                //TODO: new syntax
                throw new AppException(ErrorCodes.INVALID_EMAIL_ADDRESS, new(){email});
            }

            string hashedPasswordAndSalt = PasswordService.GetHashedPasswordAndSalt(password, user.Salt);

            if (hashedPasswordAndSalt != user.Password)
            {
                throw new AppException(ErrorCodes.INVALID_PASSWORD);
            }

            return user;
        }
    }
}
