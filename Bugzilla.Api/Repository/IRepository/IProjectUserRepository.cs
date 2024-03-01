using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Bugzilla.Api.Repository.IRepository
{
    public interface IProjectUserRepository
    {
        void AddUserToProject(ProjectUser projectUser);
        Task<bool> CheckProjectUserExist(ProjectUserDTO projectUserDto);
        Task<IEnumerable<ApplicationUser>> GetUsersByProjectId(int projectId, string role);
        Task RemoveUserToProject(int projectUserId);
        Task<ProjectUser> GetProjectUsersById(int projectUserId);
        Task<int> GetId(int projectId, string userId);

    }
}
