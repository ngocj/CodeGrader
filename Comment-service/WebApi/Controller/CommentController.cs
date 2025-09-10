using Application.Dtos.CommentDto;
using Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComment()
        {
            var result = await _commentService.GetAllCommentAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var result = await _commentService.GetCommentById(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto comment)
        {
            var result = await _commentService.AddCommentAsync(comment);
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto comment)
        {
            var result = await _commentService.UpdateCommentAsync(comment);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentService.DeleteCommentAsync(id);
            return Ok(result);
        }
        [HttpGet("problem/{problemId}")]
        public async Task<IActionResult> GetCommentsByProblemId(int problemId)
        {
            var result = await _commentService.GetCommentsByProblemId(problemId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("like/{commentId}")]
        public async Task<IActionResult> LikeComment(int commentId)
        {
            var result = await _commentService.LikeComment(commentId);
            return Ok(result);
        }
        [HttpGet("sort/like/{problemId}")]
        public async Task<IActionResult> SortCommentByLike(int problemId)
        {
            var result = await _commentService.SortCommentByLike(problemId);
            return Ok(result);
        }

        [HttpGet("sort/createdAt/{problemId}")]
        public async Task<IActionResult> SortCommentByCreatedAt(int problemId)
        {
            var result = await _commentService.SortCommentByCreateAt(problemId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("unlike/{commentId}")]
        public async Task<IActionResult> UnLikeComment(int commentId)
        {
            var result = await _commentService.UnLikeComment(commentId);
            return Ok(result);
        }
    }
}
