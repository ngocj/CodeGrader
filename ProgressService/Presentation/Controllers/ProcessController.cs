using Application.Dtos.Resquest;
using Application.Services.Interface;
using Common.ResultPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IProblemStatsService _problemStatsService;
        private readonly IUserProgressService _userProgressService;
        private readonly ISubmissionService _submissionService;
        public ProcessController(IProblemStatsService problemStatsService, IUserProgressService userProgressService, ISubmissionService submissionService)
        {
            _problemStatsService = problemStatsService;
            _userProgressService = userProgressService;
            _submissionService = submissionService;
        }

        [HttpPost]
        public async Task<Result<string>> AddProgress(GradedResult gradedResult)
        {
            var submissionResult = await _submissionService.AddSubmission(gradedResult);
            if (!submissionResult.IsSuccess)
            {
                return Result<string>.Failure("Add submission failed");
            }

            var problemStatsResult = await _problemStatsService.AddProblemStatAsync(gradedResult);
            if (!problemStatsResult.IsSuccess)
            {
                return Result<string>.Failure("Add problemStats failed");
            }

            var userProgressResult = await _userProgressService.AddUserProgressAsync(gradedResult);
            if (!userProgressResult.IsSuccess)
            {
                return Result<string>.Failure("Add user progress failed");
            }

            return Result<string>.Success("Add process successfully",null);
        }

    }

}
