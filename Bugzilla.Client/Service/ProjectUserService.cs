using Bugzilla.Client.Service.IService;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace Bugzilla.Client.Service
{
    public class ProjectUserService : IProjectUserService
    {
        private readonly HttpClient _httpClient;

        public ProjectUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CheckProjectUserExit(ProjectUserDTO projectUserDTO)
        {
            var res = await _httpClient.PostAsJsonAsync($"api/ProjectUser/ProjectUserExist", projectUserDTO);
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

        public void AddUserToProject(ProjectUserDTO projectUserDto)
        {
            var response = _httpClient.PostAsJsonAsync("api/ProjectUser", projectUserDto);

        }
        public async Task DeleteUserFromProject(int projUserId)
        {
            var response = await _httpClient.DeleteAsync($"api/ProjectUser/{projUserId}");

        }

        public async Task<IEnumerable<UserDTO>> GetUsersByProjectId(int projectId, string role)
        {

            var res = await _httpClient.GetAsync($"api/ProjectUser/{projectId}/{role}");
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
        public async Task<ProjectUserDTO> GetProjectUserById(int projectId)
        {

            var res = await _httpClient.GetAsync($"api/ProjectUser/{projectId}/ProjectUser");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<ProjectUserDTO>();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }
        }
        public async Task<int> GetId(int projectId, string UserId)
        {

            var res = await _httpClient.GetAsync($"api/ProjectUser/{projectId}/{UserId}/ProjectUser");
            if (res.IsSuccessStatusCode)
            {
                string str = await res.Content.ReadAsStringAsync();
                return Convert.ToInt32(str);
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{res.StatusCode} Message - {message}");
            }
        }

    }
}
