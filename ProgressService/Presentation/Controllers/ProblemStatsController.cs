using Application.Dtos.Resquest;
using Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemStatsController : ControllerBase
    {
        private readonly IProblemStatsService _problemStatsService;

        public ProblemStatsController(IProblemStatsService problemStatsService)
        {
            _problemStatsService = problemStatsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProblemStats(GradedResult gradedResult)
        {
            var result = await _problemStatsService.AddProblemStatAsync(gradedResult);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProblemStats()
        {
            var result = await _problemStatsService.GetAllProblemStatsAync();

            return Ok(result);
        }

        [HttpGet("{problemId}")]
        public async Task<IActionResult> GetProblemStats(int problemId)
        {
            var result = await _problemStatsService.GetProblemStatsByIdAync(problemId);

            return Ok(result);
        }
    }
}
