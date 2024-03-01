using Bugzilla.Shared;
using Bugzilla.Client.Service.IService;
using System.Net.Http.Json;
using Bugzilla.Shared.DTOs;
using Blazored.LocalStorage;
using System.Net.Http;

namespace Bugzilla.Client.Service
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ProjectService(HttpClient httpClient, ILocalStorageService localStorageService) {
           _httpClient = httpClient;
            _localStorage = localStorageService;
        }
        public void AddProject(ProjectDTO projectDTO)
        {
          var response =  _httpClient.PostAsJsonAsync<ProjectDTO>("api/Project/Create", projectDTO);
          
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0} ", token));
            }
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDTO>>("api/Project/AllProjects");
            
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsByUserId(string Id)
        {
            var res = await _httpClient.GetAsync($"api/Project/{Id}/Projects");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<IEnumerable<ProjectDTO>>();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }

        }
        public async Task<ProjectDTO> GetProjectById(int Id)
        {
            var res = await _httpClient.GetAsync($"api/Project/{Id}");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<ProjectDTO>();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }

        }

        public async void UpdateProject(ProjectDTO projectDTO)
        {
            try
            {
                await _httpClient.PutAsJsonAsync("api/Project", projectDTO);
            }
            catch(Exception ex) {
                throw;
            }
        }
        public async Task DeleteProject(int projectId)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/Project/{projectId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
