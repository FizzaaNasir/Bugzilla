using AutoMapper;
using Bugzilla.Api.Repository;
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
    public class BugController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBugRepository _bugRepository;

        public BugController(IMapper mapper, IBugRepository bugRepository)
        {
            _mapper = mapper;
            _bugRepository = bugRepository;
        }


        [HttpPost("Create")]
        [Authorize(Roles = "QA")]
        public async Task<ActionResult> Add([FromBody] BugDTO bugDTO)
        {

            if (bugDTO == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var BugMap = _mapper.Map<Bug>(bugDTO);
            try
            {
                _bugRepository.Add(BugMap);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<IEnumerable<BugDTO>>> GetBugsByProject(int Id)
        {
            try
            {
                var Bugs = _mapper.Map<IEnumerable<BugDTO>>(_bugRepository.GetBugsByProject(Id));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(Bugs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{Id}/Details")]
        //Every role is allowed to Get Bug
        public async Task<ActionResult<BugDTO>> GetBugById(int Id)
        {
            try
            {
                var project = _mapper.Map<BugDTO>(_bugRepository.GeBugById(Id));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        //Every role is allowed to update the bug status
        public async Task<ActionResult> Update([FromBody] BugDTO bugDTO)
        {

            if (bugDTO == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var BugMap = _mapper.Map<Bug>(bugDTO);
            try
            {
                _bugRepository.Update(BugMap);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpGet]
        //Every role is allowed to update the bug status
        public async Task<ActionResult> testapi()
        {

            return Ok();
        }
    }
}
