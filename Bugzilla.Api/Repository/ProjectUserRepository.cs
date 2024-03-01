using Bugzilla.Api.Data;
using Bugzilla.Api.Repository.IRepository;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bugzilla.Api.Repository
{
    public class ProjectUserRepository : IProjectUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _db;
        public ProjectUserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager; 
        }
      
        public void AddUserToProject(ProjectUser projectUser)
        {
           
            _db.Project_User.Add(projectUser);
            _db.SaveChanges();
          
        }
        public async Task RemoveUserToProject(int projectUserId)
        {
            ProjectUser projectUser = await GetProjectUsersById(projectUserId);
            if (projectUser != null)
            {
                 _db.Project_User.Remove(projectUser);
                 _db.SaveChanges();
            }
        }
        public async Task<bool> CheckProjectUserExist(ProjectUserDTO projectUserDto)
        {
            return _db.Project_User.Any(e => e.UserId == projectUserDto.UserId && e.ProjectId == projectUserDto.ProjectId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersByProjectId(int projectId, string role)
        {
            var developers = new List<ApplicationUser>();
            var Users = await _db.Project_User.Where(u => u.ProjectId == projectId).Include(u => u.User).Select(u => u.User).ToListAsync();
            foreach(var user in Users)
            {
                var UserInRole = await _userManager.IsInRoleAsync(user, role);
                if (UserInRole)
                {
                    developers.Add(user);
                }
            }
            return developers; 
        }

        public async Task<ProjectUser> GetProjectUsersById(int projectUserId)
        {
            ProjectUser projectUser = _db.Project_User.Where(u => u.Id == projectUserId).FirstOrDefault();
            return projectUser;
        }


        public async Task<int> GetId(int projectId, string userId)
        {
            ProjectUser ProjectUser = _db.Project_User.Where(u => u.ProjectId == projectId && u.UserId == userId).FirstOrDefault();
            if(ProjectUser != null)
            {
              return ProjectUser.Id;
            }
            return 0;
        }

    }
}
