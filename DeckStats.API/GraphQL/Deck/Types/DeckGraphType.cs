using DeckStats.Data.Models;

namespace DeckStats.API.GraphQL.Types;

public class DeckGraphType : ObjectType<Deck>
{
    protected override void Configure(IObjectTypeDescriptor<Deck> descriptor)
    {
        descriptor.Field(f => f.Name);
        descriptor.Field(f => f.Wins);
        descriptor.Field(f => f.Losses);
    }
}