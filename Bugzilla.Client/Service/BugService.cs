using Blazored.LocalStorage;
using Bugzilla.Client.Service.IService;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace Bugzilla.Client.Service
{
    public class BugService : IBugService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public BugService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorage = localStorageService;
          
        }
        public async Task AddBugToProject(BugDTO bugDTO)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            _httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0} ", token));
            var response = _httpClient.PostAsJsonAsync<BugDTO>("api/Bug/Create", bugDTO);
        }

        public async Task<IEnumerable<BugDTO>> GetBugsByProject(int projectId)
        {
            var res = await _httpClient.GetAsync($"api/Bug/{projectId}");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<IEnumerable<BugDTO>>();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }
        }
        public async Task<BugDTO> GetBugById(int Id)
        {
            // var res = await _httpClient.GetFromJsonAsync<ProjectDTO>($"api/Project/{Id}");
            var res = await _httpClient.GetAsync($"api/Bug/{Id}/Details");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<BugDTO>();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }
            // return res;

        }

        public async void UpdateBug(BugDTO bugDTO)
        {
            try
            {
                await _httpClient.PutAsJsonAsync("api/Bug", bugDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
