using DeckStats.API.GraphQL;
using DeckStats.API.Utils.ExceptionHandling;
using DeckStats.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DeckStats.API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddDbContext<DeckStatsDbContext>(opts => opts.UseInMemoryDatabase("DeckStatsDb"));
builder.Services.AddGraphQLServer()
    .ModifyRequestOptions(x => x.IncludeExceptionDetails = true)
    .AddQueryType<Queries>()
    .AddMutationType<Mutations>()
    .AddErrorFilter<GraphQLErrorFilter>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
