using Application.Dtos.Resquest;
using Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProgressController : ControllerBase
    {
        private readonly IUserProgressService _userProgressService;

        public UserProgressController(IUserProgressService userProgressService)
        {
            _userProgressService = userProgressService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserProgress(GradedResult gradedResult)
        {
            var result = await _userProgressService.AddUserProgressAsync(gradedResult);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProgress(int userId)
        {
            var result = await _userProgressService.GetUserProgress(userId);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProgress()
        {
            var result = await _userProgressService.GetUserProgress();

            return Ok(result);
        }
    }
}
