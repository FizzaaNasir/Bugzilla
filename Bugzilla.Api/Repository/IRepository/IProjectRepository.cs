using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Bugzilla.Api.Repository.IRepository
{
    public interface IProjectRepository
    {
        void Add(Project project);
        void Update(Project project);
        void Delete(Project project);
        IEnumerable<Project> GetProjects();
        Project GetProjectById(int id);
        IEnumerable<Project> GetProjectsByUserId(string userId);
    }
}
