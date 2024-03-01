using Bugzilla.Client.Service.IService;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Bugzilla.Client.Pages.Manager
{
    public class ProjectBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        public IEnumerable<UserDTO> DevUserDTOlist { get; set; }
        public IEnumerable<UserDTO> QAUserDTOlist { get; set; }
        public IEnumerable<BugDTO> Bugs { get; set; }

        public ProjectDTO ProjectDTO = new ProjectDTO();

        public ProjectUserDTO ProjectUserDTO = new ProjectUserDTO();

        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IProjectService ProjectService { get; set; }
        [Inject]
        public IProjectUserService ProjectUserService { get; set; }
        [Inject]
        public IBugService BugService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        //states
        public Dictionary<string, bool> QASelectedStates = new Dictionary<string, bool>();
        public Dictionary<string, bool> DevSelectedStates = new Dictionary<string, bool>();

        protected override async Task OnInitializedAsync()
        {
            DevUserDTOlist = await UserService.GetUsersByRole("Developer");
            QAUserDTOlist = await UserService.GetUsersByRole("QA");
            ProjectDTO = await ProjectService.GetProjectById(Id);
            Bugs = await BugService.GetBugsByProject(Id);

            // Initialize the selected states dictionary
            foreach (var dev in DevUserDTOlist)
            {
                DevSelectedStates.Add(dev.Id, false);
            }

            foreach (var qa in QAUserDTOlist)
            {
                QASelectedStates.Add(qa.Id, false);
            }
            foreach (var dev in DevUserDTOlist)
            {
                if (await CheckIfUserAndProjectExist(dev.Id, Id))
                {
                   DevSelectedStates[dev.Id] = true;
                }
            }

            foreach (var qa in QAUserDTOlist)
            {
                if (await CheckIfUserAndProjectExist(qa.Id, Id))
                {
                   QASelectedStates[qa.Id] = true;
                }
            }
            
        }

        public async void QASelectedState(UserDTO user)
        {
            QASelectedStates[user.Id] = !QASelectedStates[user.Id];
            if (QASelectedStates[user.Id])
            {
                ProjectUserDTO.UserId = user.Id;
                ProjectUserDTO.ProjectId = Id;


                if (!await ProjectUserService.CheckProjectUserExit(ProjectUserDTO))
                {
                     ProjectUserService.AddUserToProject(ProjectUserDTO);
                }
            }
            else
            {
                // Remove the project user when the state is toggled to false
                await ProjectUserService.DeleteUserFromProject(ProjectUserDTO.Id);
            }
        }

        public async Task DevSelectedStateAsync(UserDTO user)
        {
            DevSelectedStates[user.Id] = !DevSelectedStates[user.Id];
            if (DevSelectedStates[user.Id])
            {
                ProjectUserDTO.UserId = user.Id;
                ProjectUserDTO.ProjectId = Id;


                if (!await ProjectUserService.CheckProjectUserExit(ProjectUserDTO))
                {
                    ProjectUserService.AddUserToProject(ProjectUserDTO);
                }
            }
            else
            {


                //idot, make func which fetch u an id based on projectid and userid=========== now sleeeep
                int projectUserId = await ProjectUserService.GetId(ProjectUserDTO.ProjectId, user.Id);
                    
                    // Remove the project user when the state is toggled to false
                    await ProjectUserService.DeleteUserFromProject(projectUserId);
                
            }
        }


        private async Task<bool> CheckIfUserAndProjectExist(string userId, int projectId)
        {
            
            ProjectUserDTO.ProjectId = projectId;
            ProjectUserDTO.UserId = userId;


            return await ProjectUserService.CheckProjectUserExit(ProjectUserDTO);
        }

        protected void NavigateToBugDetailPage(int Id)
        {
            navigationManager.NavigateTo($"/BugDetails/{Id}");
        }
    }
}
