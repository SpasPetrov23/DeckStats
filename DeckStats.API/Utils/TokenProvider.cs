using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeckStats.Data;
using DeckStats.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace DeckStats.API.Utils;

public class TokenProvider
{
    private readonly IConfiguration _configuration;
    private readonly DeckStatsDbContext _dbContext;

    public TokenProvider(IConfiguration configuration, DeckStatsDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public string GenerateJwtToken(User user)
    {
        byte[] key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
        
        JwtSecurityToken jwToken = new(
            _configuration["AppSettings:JWTIssuer"],
            _configuration["AppSettings:JWTAudience"],
            GetUserClaims(user),
            new DateTimeOffset(DateTime.Now).DateTime,
            new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
            new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwToken);

        return token;
    }
    
    private IEnumerable<Claim> GetUserClaims(User user)
    {
        IEnumerable<Claim> claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
        };
        return claims;
    }
}