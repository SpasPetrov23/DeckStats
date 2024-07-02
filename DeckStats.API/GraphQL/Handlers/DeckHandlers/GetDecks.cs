using DeckStats.API.GraphQL.Dtos;
using DeckStats.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeckStats.API.GraphQL.Handlers.DeckHandlers;

public class GetDecks
{
    public class Query : IRequest<Result>
    {
    }

    public record Result(List<DeckDto> Decks);

    public class Handler : IRequestHandler<Query, Result>
    {
        private readonly DeckStatsDbContext _dbContext;

        public Handler(DeckStatsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            List<DeckDto> decks = await _dbContext.Decks.Select(x => new DeckDto()
            {
                Name = x.Name,
                Wins = x.Wins,
                Losses = x.Losses
            }).ToListAsync(cancellationToken: cancellationToken);
            
            return new(decks);
        }
    }
}
