using Bugzilla.Client.Service.IService;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace Bugzilla.Client.Pages.SharedPages
{
    public class ProjectDetailBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        public IEnumerable<BugDTO> Bugs { get; set; }

        public ProjectDTO ProjectDTO { get; set; }

        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IProjectService ProjectService { get; set; }
        [Inject]
        public IBugService BugService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IAuthService authService { get; set; }

        public ClaimsPrincipal User { get; set; }


        protected override async Task OnInitializedAsync()
        {
            User = await authService.GetClaimsPrincipalUser();
            ProjectDTO = await ProjectService.GetProjectById(Id);
            Bugs = await BugService.GetBugsByProject(Id);
        }
        protected void NavigateToBugDetailPage(int Id)
        {
            navigationManager.NavigateTo($"/BugDetails/{Id}");
        }
        protected void NavigateToCreateBugPage()
        {
            navigationManager.NavigateTo($"/CreateBug/{Id}");
        }
    }
}
