using DeckStats.API.Controllers.Dtos;
using DeckStats.API.Utils;
using DeckStats.Data;
using DeckStats.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeckStats.API.Services;

public class AccountService
{
    private readonly IConfiguration _config;
    private readonly TokenProvider _tokenProvider;
    private readonly DeckStatsDbContext _dbContext;

    public AccountService(IConfiguration config, DeckStatsDbContext dbContext)
    {
        _config = config;
        _tokenProvider = new(_config, _dbContext);
        _dbContext = dbContext;
    }
    
    public async Task<string> Login(UserLoginRequest userLoginInfo)
    {
        User user = await Authenticate(userLoginInfo.Email, userLoginInfo.Password);
        
        string jwtToken = _tokenProvider.GenerateJwtToken(user);

        return jwtToken;
    }
    
    private async Task<User> Authenticate(string email, string password)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        
        if (user is null)
        {
            // Invalid Email
            throw new Exception();
        }

        string hashedPasswordAndSalt = PasswordService.GetHashedPasswordAndSalt(password, user.Salt);

        if (hashedPasswordAndSalt != user.Password)
        {
            // Invalid Password
            throw new Exception();
        }

        return user;
    }
}
