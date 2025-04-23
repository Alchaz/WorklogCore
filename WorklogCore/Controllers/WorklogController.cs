using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using WorklogCore.Models;

namespace WorklogCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorklogController : ControllerBase
    {
        private readonly IWorklogService _worklogService;

        public WorklogController(IWorklogService worklogService)
        {
            _worklogService = worklogService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Worklog worklog)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                await _worklogService.CreateWorklog(worklog, token);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string filter = "ALL", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var result = await _worklogService.GetWorklogsGroupedByDay(token, filter, page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet("WorklogsByUser")]
        public async Task<IActionResult> WorklogsByUser()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var result = await _worklogService.GetWorklogs(token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchWorkedHours(int id, [FromBody] UpdateWorkedHoursDto hoursDto)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                 await _worklogService.UpdateWorkedHours(id, hoursDto.WorkedHours, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
