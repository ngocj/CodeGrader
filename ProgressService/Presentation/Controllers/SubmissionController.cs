using Application.Dtos.Resquest;
using Application.Dtos.SubmisstionDtos;
using Application.Services.Implementation;
using Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService submisstionService;

        public SubmissionController(ISubmissionService submisstionService)
        {
            this.submisstionService = submisstionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubmission(GradedResult gradedResult)
        {
            var result = await submisstionService.AddSubmission(gradedResult);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubmissions()
        {
            var result = await submisstionService.GetAllSubmission();
            return Ok(result);
        }

        [HttpGet("{submissionId}")]
        public async Task<IActionResult> GetSubmissionById(int submissionId)
        {
            var result = await submisstionService.GetSubmisstion(submissionId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("userId")]
        public async Task<IActionResult> GetSubmissionsByUserId()
        {
            var result = await submisstionService.GetSubmisstionsByUserId();
            return Ok(result);
        }

        [HttpGet("problem/{problemId}")]
        public async Task<IActionResult> GetSubmissionsByProblemId(int problemId)
        {
            var result = await submisstionService.GetSubmisstionsByProblemId(problemId);
            return Ok(result);
        }


        [HttpDelete("{submissionId}")]
        public async Task<IActionResult> DeleteSubmission(int submissionId)
        {
            var result = await submisstionService.DeleteSubmisstion(submissionId);
            return Ok(result);
        }

    }
}
