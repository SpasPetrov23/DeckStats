using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace DeckStats.UI.Helpers
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
        {
            _localStorageService = localStorageService;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _localStorageService.GetItemAsStringAsync("token");

            ClaimsIdentity identity = new();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                identity = new(ParseClaimsFromJwt(token), "jwt");
                _http.DefaultRequestHeaders.Authorization = new("Bearer", token.Replace("\"", ""));
            }

            ClaimsPrincipal user = new(identity);
            AuthenticationState state = new(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            string payload = jwt.Split('.')[1];
            Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(Base64UrlEncoder.Decode(payload));
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
    }
}
