using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using System.Net.Http;

namespace Bugzilla.Client.Service.IService
{
    public interface IProjectUserService
    {
        public void AddUserToProject(ProjectUserDTO projectUserdto);
        Task<bool> CheckProjectUserExit(ProjectUserDTO projectUserDTO);
        Task<IEnumerable<UserDTO>> GetUsersByProjectId(int projectId, string role);
        Task DeleteUserFromProject(int projUserId);
        Task<ProjectUserDTO> GetProjectUserById(int projectId);
        Task<int> GetId(int projectId, string UserId);
    }
}
