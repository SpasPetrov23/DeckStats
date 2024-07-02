using DeckStats.API.Controllers.Dtos;
using DeckStats.API.Utils;
using DeckStats.Data;
using DeckStats.Data.Models;

namespace DeckStats.API.Services;

public class AccountService
{
    private readonly IConfiguration _config;
    private readonly TokenProvider _tokenProvider;
    private readonly EncryptionHelper _encryptionHelper;
    private readonly DeckStatsDbContext _dbContext;

    public AccountService(IConfiguration config, EncryptionHelper encryptionHelper, DeckStatsDbContext dbContext)
    {
        _config = config;
        _tokenProvider = new(_config, _dbContext);
        _encryptionHelper = encryptionHelper;
        _dbContext = dbContext;
    }
    
    public string Login(UserLoginRequest userLoginInfo)
    {
        User user = Authenticate(userLoginInfo.Email, userLoginInfo.Password);
        
        string jwtToken = _tokenProvider.GenerateJwtToken(user);

        return jwtToken;
    }
    
    private User Authenticate(string email, string password)
    {
        User? user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
        
        if (user is null)
        {
            // Invalid Email
            throw new Exception();
        }

        string hashedPasswordAndSalt = _encryptionHelper.GetHashedPasswordAndSalt(password, user.Salt);

        if (hashedPasswordAndSalt != user.Password)
        {
            // Invalid Password
            throw new Exception();
        }

        return user;
    }
}
