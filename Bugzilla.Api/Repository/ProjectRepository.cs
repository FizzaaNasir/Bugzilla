using Bugzilla.Api.Data;
using Bugzilla.Api.Repository.IRepository;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bugzilla.Api.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _db;
        public ProjectRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Project project)
        {
            try
            {
                _db.Add(project);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Project GetProjectById(int id)
        {
            return _db.Project.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Project> GetProjects()
        {
            return _db.Project.ToList();
        }

        public void Update(Project project)
        {
            _db.Project.Update(project);
            _db.SaveChanges();
        }
        public void Delete(Project project)
        {
            _db.Project.Remove(project);
            _db.SaveChanges();
        }

        public IEnumerable<Project> GetProjectsByUserId(string userId)
        {
            var projects = _db.Project
                .Where(p => _db.Project_User
                            .Any(pu => pu.UserId == userId && pu.ProjectId == p.Id)).ToList();
            return projects;
        }
    }
}
