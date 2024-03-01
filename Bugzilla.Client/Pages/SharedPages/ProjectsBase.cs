using Bugzilla.Client.Service;
using Bugzilla.Client.Service.IService;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Bugzilla.Client.Pages.SharedPages
{
    public class ProjectsBase : ComponentBase
    {
        [Parameter]
        public string userId { get; set; }
        public IEnumerable<ProjectDTO> Projects { get; set; }
        [Inject]
        public IProjectService ProjectService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Projects = await ProjectService.GetProjectsByUserId(userId);
            }
            catch (Exception ex)
            {

                // throw ex.Message;
            }
        }
        protected void NavigateToProjectDetailsPage(int Id)
        {

            navigationManager.NavigateTo($"ProjectDetail/{Id}");
        }
    }
}
