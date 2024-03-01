using Bugzilla.Client.Pages.Manager;
using Bugzilla.Client.Service;
using Bugzilla.Client.Service.IService;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace Bugzilla.Client.Pages.QA
{
    public class CreateBugBase : ComponentBase
    {
        //ProjectId
        [Parameter]
        public int Id { get; set; }

        public BugDTO BugDTO = new BugDTO();
        public ProjectUserDTO ProjectUserDTO = new ProjectUserDTO();
        public IEnumerable<UserDTO> UserDTO { get; set; }
        
        [Inject]
        public IBugService BugService { get; set; }
        [Inject]
        public IProjectUserService ProjectUserService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public IAuthService authService { get; set; }

        public ClaimsPrincipal User { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {

                User = await authService.GetClaimsPrincipalUser();
                UserDTO = await ProjectUserService.GetUsersByProjectId(Id, "Developer");
          
            }
            catch (Exception ex)
            {

                // throw ex.Message;
            }
        }
        public void CreateNewBug()
        {
            try
            {
                BugDTO.CreaterId = "4bce256c-7776-451a-9dea-e64095a509d3";
                BugDTO.ProjectId = Id;
               
                BugService.AddBugToProject(BugDTO);
                //navigationManager.NavigateTo("QA / Projects /{ userId}");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
