using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using UserManagement.ViewModel.Users;

namespace BlogLand.UI.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        #region Cookie Auth

        private readonly HttpClient _http;

        public CustomAuthStateProvider(HttpClient http)
        {
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var currentUser = await _http.GetFromJsonAsync<AuthViewModel>("userApi/Auth/CurrentAccountInfo");

            var claimsIdentity = new ClaimsIdentity();

            if (currentUser != null && !string.IsNullOrWhiteSpace(currentUser.UserName))
            {
                var claims = new List<Claim>
                {
                    new Claim("AccountId", currentUser.Id.ToString()),
                    new Claim("UserName", currentUser.UserName),
                    new Claim(ClaimTypes.Role, currentUser.RoleId.ToString()),
                };

                claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            }

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            var state = new AuthenticationState(claimsPrincipal);
         
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            
            return state;
        }
        #endregion

        #region Jwt Auth
        //private readonly ILocalStorageService _localStorage;
        //private readonly HttpClient _http;

        //public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        //{
        //    _localStorage = localStorage;
        //    _http = http;
        //}
        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    string token = await _localStorage.GetItemAsStringAsync("token");

        //    var identity = new ClaimsIdentity();
        //    _http.DefaultRequestHeaders.Authorization = null;

        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        //        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    }


        //    var user = new ClaimsPrincipal(identity);
        //    var state = new AuthenticationState(user);

        //    NotifyAuthenticationStateChanged(Task.FromResult(state));

        //    return state;
        //}

        //public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        //{
        //    var payload = jwt.Split('.')[1];
        //    var jsonBytes = ParseBase64WithoutPadding(payload);
        //    var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        //    return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        //}

        //private static byte[] ParseBase64WithoutPadding(string base64)
        //{
        //    string converted = base64.Replace('-', '+');
        //    converted = converted.Replace('_', '/');
        //    switch (converted.Length % 4)
        //    {
        //        case 2: base64 += "=="; break;
        //        case 3: base64 += "="; break;
        //    }
        //    return Convert.FromBase64String(base64);
        //}
        #endregion
    }
}

