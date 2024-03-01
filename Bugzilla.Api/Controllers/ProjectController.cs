using AutoMapper;
using Bugzilla.Api.Repository.IRepository;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bugzilla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IMapper mapper, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Add(ProjectDTO projectDTO)
        {

            if (projectDTO == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var projectMap = _mapper.Map<Project>(projectDTO);
            try
            {
                _projectRepository.Add(projectMap);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Update([FromBody] ProjectDTO projectDTO)
        {

            if (projectDTO == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var projectMap = _mapper.Map<Project>(projectDTO);
            try
            {
                _projectRepository.Update(projectMap);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpDelete]
        [Route("{projectId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Delete(int projectId)
        {
            Project project = _projectRepository.GetProjectById(projectId);

            if (project == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();
           
            try
            {
                _projectRepository.Delete(project);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("AllProjects")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            try
            {
                var projects = _mapper.Map<List<ProjectDTO>>(_projectRepository.GetProjects());
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<ProjectDTO>> GetProjectById(int Id)
        {
            try
            {
                var project = _mapper.Map<ProjectDTO>(_projectRepository.GetProjectById(Id));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{Id}/Projects")]
        //[Authorize(Roles = "Developer, QA")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjectsByUserId(string Id)
        {
            try
            {
                var projects = _mapper.Map<List<ProjectDTO>>(_projectRepository.GetProjectsByUserId(Id));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
