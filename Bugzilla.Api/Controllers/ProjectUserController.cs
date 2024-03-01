using AutoMapper;
using Bugzilla.Api.Repository;
using Bugzilla.Api.Repository.IRepository;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Bugzilla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectUserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectUserRepository _projectUserRepostory;

        public ProjectUserController(IMapper mapper, IProjectUserRepository projectUserRepository )
        {
            _mapper = mapper;
            _projectUserRepostory = projectUserRepository;
        }
        [HttpPost]
        //[Authorize(Roles = "Manager")]
        public async Task<ActionResult> AddUserToProject(ProjectUserDTO projectUserDto)
        {
            if (projectUserDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();
            var projectUserMap = _mapper.Map<ProjectUser>(projectUserDto);
            try
            {
                _projectUserRepostory.AddUserToProject(projectUserMap);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpDelete]
        [Route("{projectUserID}")]
      //  [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DeleteUserFromProject(int projectUserID)
        {
            try
            {
                _projectUserRepostory.RemoveUserToProject(projectUserID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        /// <summary>
        /// This api checks if the user is alredy assigned to a particular project and vice versa
        /// </summary>
        /// <param name="projectUserDto"></param>
        /// <returns></returns>

        [HttpPost("ProjectUserExist")]
        public async Task<ActionResult<bool>> ProjectUserExist ([FromBody]ProjectUserDTO projectUserDto)
        {
            if (projectUserDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();
          
            try
            {
                bool exist = await _projectUserRepostory.CheckProjectUserExist(projectUserDto);

                return exist == true ? Ok(true) : Ok(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpGet]
        [Route("{projectId}/{role}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>>GetUsersByProjectId(int projectId, string role)
        {
            try
            {
                var users = await  _projectUserRepostory.GetUsersByProjectId(projectId, role);
                //var users = _mapper.Map<IEnumerable<UserDTO>>(_projectUserRepostory.GetUsersByProjectId(projectId, role));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{projectId}/ProjectUser")]
        public async Task<ActionResult<ProjectUserDTO>> GetProjectUserById(int projectId)
        {
            try
            {
               
                var ProjectUserDTO = _mapper.Map<ProjectUserDTO>(_projectUserRepostory.GetProjectUsersById(projectId));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(ProjectUserDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{projectId}/{userId}/ProjectUser")]
        public async Task<ActionResult<int>> GetId(int projectId, string userId)
        {
            try
            {
                int ProjectUserId = await _projectUserRepostory.GetId(projectId, userId);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(ProjectUserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
