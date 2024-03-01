using Bugzilla.Api.Data;
using Bugzilla.Api.Repository.IRepository;
using Bugzilla.Shared;

namespace Bugzilla.Api.Repository
{
    public class BugRepository : IBugRepository
    {
        private readonly ApplicationDbContext _db;
        public BugRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Bug Bug)
        {
           _db.Add(Bug);
           _db.SaveChanges();
        }

        public void Update(Bug bug)
        {
            _db.Bug.Update(bug);
            _db.SaveChanges();
        }
        public Bug GeBugById(int id)
        {
            return _db.Bug.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Bug> GetBugsByProject(int projectId)
        {
            var x = _db.Bug.Where(u => u.ProjectId == projectId).ToList();
            if (x != null)
            {
                return x;
            }
            return x;
        }
    }
}
