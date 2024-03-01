using Bugzilla.Client.Service;
using Bugzilla.Client.Service.IService;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace Bugzilla.Client.Pages.Manager
{

    public class BugDetailsBase : ComponentBase
    {

        [Parameter]
        public int Id { get; set; }
        public BugDTO BugDTO = new BugDTO();
        public string Assignee { get; set; }
        public string Creater { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IBugService BugService { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        protected Dictionary<string, bool> statusStates = new Dictionary<string, bool>
        {
            { "New", false },
            { "Started", false },
            { "Completed", false},
            { "Resolved" , false }
        };

        // Method to handle the click event of list items and update the selected status
        protected void OnStatusClicked(string status)
        {
            foreach (var key in statusStates.Keys.ToList())
            {
                statusStates[key] = key == status;
            }
            BugDTO.Status = status;

            // Call the update method to update the bug in the database
             BugService.UpdateBug(BugDTO);
        }
        protected override async Task OnInitializedAsync()
        {
            BugDTO = await BugService.GetBugById(Id);

            var assigneeId = BugDTO.AssigneeId;
            var creatorId = BugDTO.CreaterId;

            ///  Assignee = await UserService.GetUserById(assigneeId);
             Assignee = await UserService.GetUserById(assigneeId);
            Console.WriteLine("Assignee:" + Assignee);
            // Creater = await UserService.GetUserById(creatorId);

            Creater = await UserService.GetUserById(creatorId);

        }

    }
}
