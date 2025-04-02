using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text.Json;

namespace Langchips.Blazor.AuthProviders
{

    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private ClaimsPrincipal _user = new(new ClaimsIdentity());

        public AuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            _user = new ClaimsPrincipal(identity);

            return new AuthenticationState(_user);
        }

        public async Task Login(string token)
        {
            await _localStorage.SetItemAsStringAsync("authToken", token);
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            _user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _user = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var payload = token.Split('.')[1];
            var jsonBytes = Convert.FromBase64String(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
    }


    //public class AuthStateProvider : AuthenticationStateProvider
    //{
    //    //private readonly ITokenService _tokenService;

    //    //public AuthStateProvider(ITokenService tokenService)
    //    //{
    //    //    _tokenService = tokenService;
    //    //}
    //    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    //    {
    //        //var token = await _tokenService.GetTokenAsync();  // Retrieve the token from localStorage, or other storage
    //        //var identity = string.IsNullOrEmpty(token)
    //        //? new ClaimsIdentity()  // Not authenticated
    //        //: new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");  // Authenticated using JWT claims

    //        //var user = new ClaimsPrincipal(identity);
    //        //return new AuthenticationState(user);

    //        await Task.Delay(1500);
    //        var anonymous = new ClaimsIdentity();
    //        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
    //    }
    //    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    //    {
    //        var payload = jwt.Split('.')[1];
    //        var jsonBytes = WebEncoders.Base64UrlDecode(payload);
    //        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

    //        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    //    }
    //    public void MarkUserAsAuthenticated(string token)
    //    {
    //        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
    //        var user = new ClaimsPrincipal(identity);
    //        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    //    }

    //    public void MarkUserAsLoggedOut()
    //    {
    //        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    //        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    //    }
    //}
}
