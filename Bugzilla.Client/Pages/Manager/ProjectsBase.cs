using Blazored.LocalStorage;
using Bugzilla.Client.Service;
using Bugzilla.Client.Service.IService;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Bugzilla.Client.Pages.Manager
{
    public class ProjectsBase : ComponentBase
    { 

        public IEnumerable<ProjectDTO> ProjectDTO { get; set; }
        [Inject]
        public IProjectService ProjectService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public ILocalStorageService localstorage { get; set; }
        [Inject] 
        public IAuthService authService { get; set; }

        public ClaimsPrincipal User { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ProjectDTO = await ProjectService.GetProjectsAsync();
            User = await authService.GetClaimsPrincipalUser();
            Console.WriteLine(User.Identity.IsAuthenticated);
        }
        protected void NavigateToPage()
        {
            navigationManager.NavigateTo("/AddProject");
        }
        protected void NavigateToEditPage(int Id)
        {
            navigationManager.NavigateTo($"/EditProject/{Id}");
        }
        protected void NavigateToProjectPage(int Id)
        {
            navigationManager.NavigateTo($"/Project/{Id}");
        }

        protected async void DeleteProject(int projectID)
        {
           await ProjectService.DeleteProject(projectID);
            navigationManager.NavigateTo($"/Projects");
        }
    }
}
