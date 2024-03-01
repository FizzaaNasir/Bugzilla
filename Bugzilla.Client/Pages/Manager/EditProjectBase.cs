using Bugzilla.Client.Service;
using Bugzilla.Client.Service.IService;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace Bugzilla.Client.Pages.Manager
{
    public class EditProjectBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        public ProjectDTO ProjectDTO = new ProjectDTO();
        [Inject]
        public IProjectService ProjectService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IAuthService authService { get; set; }
        public ClaimsPrincipal User { get; set; }
        protected override async Task OnInitializedAsync()
        {
            User = await authService.GetClaimsPrincipalUser();
            ProjectDTO = await ProjectService.GetProjectById(Id);
        }
        public void EditProject()
        {
            try
            {
                ProjectService.UpdateProject(ProjectDTO);
                navigationManager.NavigateTo("/Projects");
            }
            catch (Exception ex)
            {

                // throw ex.Message;
            }
        }
    }
}
