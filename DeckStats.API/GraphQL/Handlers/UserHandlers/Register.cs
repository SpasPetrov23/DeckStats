using DeckStats.API.Utils;
using DeckStats.Data;
using DeckStats.Data.Models;
using MediatR;

namespace DeckStats.API.GraphQL.Handlers.UserHandlers;

public class Register
{
    public record Mutation : IRequest<Result>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public record Result(User User);

    public class Handler : IRequestHandler<Mutation, Result>
    {
        private readonly DeckStatsDbContext _dbContext;

        public Handler(DeckStatsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Mutation request, CancellationToken cancellationToken)
        {
            User newUser = new()
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            };
            
            PasswordService.EncryptPassword(newUser);

            await _dbContext.Users.AddAsync(newUser, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new(newUser);
        }
    }
}
