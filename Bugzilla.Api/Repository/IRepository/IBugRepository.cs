using Bugzilla.Shared;

namespace Bugzilla.Api.Repository.IRepository
{
    public interface IBugRepository
    {
        void Add(Bug Bug);
        void Update(Bug project);
        IEnumerable<Bug> GetBugsByProject(int projectId);
        Bug GeBugById(int id);
        //void Update(Project project);
        //IEnumerable<Project> GetProjects();
        //Project GetProjectById(int id);
    }
}
