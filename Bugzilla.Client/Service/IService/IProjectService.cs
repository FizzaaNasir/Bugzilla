
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;

namespace Bugzilla.Client.Service.IService
{
    public interface IProjectService
    {
        void AddProject(ProjectDTO projectDTO);
        void UpdateProject(ProjectDTO projectDTO);
        Task DeleteProject(int projectId);
        Task<IEnumerable<ProjectDTO>> GetProjectsAsync();
        Task<ProjectDTO> GetProjectById(int Id);
        Task<IEnumerable<ProjectDTO>> GetProjectsByUserId(string Id);

    }
}
