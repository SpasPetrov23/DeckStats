using DeckStats.Data.Models;

namespace DeckStats.Data.StaticData;

public class UserStaticData
{
    public static User GetUserStaticData()
    {
        User user = new()
        {
            Id = 1,
            Username = "testName",
            Email = "testEmail@gmail.com",
            Password = "6df3bff748c98d3229613307f46a363f63e4144c6b28e036c61ccfb308fe4d003b3944e7456e247d0a30fc6ac9d93465d2227a841da13dd6f14d9c40628ebf95",
            Salt = "5fbe233f2faf1ada"
        };

        return user;
    }
}
