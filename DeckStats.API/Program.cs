using DeckStats.API.GraphQL;
using DeckStats.API.GraphQL.Handlers;
using DeckStats.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddDbContext<DeckStatsDbContext>(opts => opts.UseInMemoryDatabase("DeckStatsDb"));
builder.Services.AddGraphQLServer()
    .ModifyRequestOptions(x => x.IncludeExceptionDetails = true)
    .AddQueryType<Queries>();
// builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.MapGraphQL();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("all");
using (IServiceScope serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    DeckStatsDbContext context = serviceScope.ServiceProvider.GetRequiredService<DeckStatsDbContext>();
    context.Database.EnsureCreated();
}

app.Run();