using Bugzilla.Client.Service.IService;
using Blazored.LocalStorage;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using System.Data;
using System.Net.Http.Json;

namespace Bugzilla.Client.Service
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException();
            _localStorage = localStorage;
        }
   
        public async Task<IEnumerable<UserDTO>> GetUsersByRole(string role)
        {
            var res = await _httpClient.GetAsync($"api/User/{role}");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<IEnumerable<UserDTO>>();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }
        }
        public async Task<string> GetUserById(string Id)
        {

            var res = await _httpClient.GetAsync($"api/User/{Id}/User");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsStringAsync();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }
        }
        public async Task<Dictionary<string, string>> GetClaimsFromToken(string tokenStr)
        {

            var res = await _httpClient.GetAsync($"api/User/{tokenStr}/claim");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }
        }

        public async Task<bool> IsUserExist(string uname)
        {
            var res = await _httpClient.GetAsync($"api/User/{uname}/Userexists");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }
        }
    }
}
