using Bugzilla.Client.Service.IService;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace Bugzilla.Client.Pages.Manager
{
    public class CreateProjectBase : ComponentBase
    {
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
        }
        public async Task CreateNewProject()
        {
            try
            {
              
               // Console.WriteLine("from create proj:"+ User.Identity.IsAuthenticated);
                ProjectService.AddProject(ProjectDTO);
                navigationManager.NavigateTo("/Projects");
            }
            catch (Exception ex)
            {

                // throw ex.Message;
            }
        }
      
    }
}
