using Bugzilla.Shared;
using Bugzilla.Client.Service.IService;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;

namespace Bugzilla.Client.Service
{
    public class AuthService :  IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IUserService _userService;
        private ClaimsPrincipal _claimsPrincipal;

        private string tokenstr { get; set; }
        //[Inject]
        //public IUserService userService { get; set; }
        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, IUserService userService)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _userService = userService;
        }
        public async Task<bool> Register (RegisterModel input) 
        {
 
               var response = await _httpClient.PostAsJsonAsync("api/Auth/Register", input);
              
                    return response.IsSuccessStatusCode;
        
        }
        public async Task<string> Login(LoginModel input)
        {
            try
            {   // returns authentication token
                var responsetoken = await _httpClient.PostAsJsonAsync("api/Auth/Login", input);
                var token = await responsetoken.Content.ReadAsStringAsync();
                
                if (responsetoken.IsSuccessStatusCode)
                {
                    await _localStorage.SetItemAsync("authToken", token);
                    return await responsetoken.Content.ReadAsStringAsync();
                }
                 return await responsetoken.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return "Failed";
            }

        }

        public async Task<ClaimsPrincipal> GetClaimsPrincipalUser()
        {
            // Retrieve or create the ClaimsPrincipal based on token validation or local storage
            tokenstr = await _localStorage.GetItemAsync<string>("authToken");

            if (tokenstr == null)
            {
                return null;
            }
            var claims = await _userService.GetClaimsFromToken(tokenstr);  // Example: Get from local storage or API

            var claimsIdentity = new ClaimsIdentity(claims.Select(c => new Claim(c.Key, c.Value)), "token");

            return new ClaimsPrincipal(claimsIdentity);
            
        }
    }
}
