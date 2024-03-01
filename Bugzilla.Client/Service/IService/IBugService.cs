using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;

namespace Bugzilla.Client.Service.IService
{
    public interface IBugService
    {
        Task AddBugToProject(BugDTO bugDTO);
        void UpdateBug(BugDTO bugDTO);

        Task<IEnumerable<BugDTO>> GetBugsByProject(int projectId);

        Task<BugDTO> GetBugById(int Id);
    }
}
