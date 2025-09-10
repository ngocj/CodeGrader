using Application.Dtos.CommentDto;
using Common;
using Domain.Entities;

namespace Application.Service.Interface
{
    public interface ICommentService
    {
        Task<Result<IEnumerable<CommentViewDto>>> GetAllCommentAsync();
        Task<Result<CommentViewDto>> GetCommentById(int id);
        Task<Result<CommentViewDto>> AddCommentAsync(CreateCommentDto createCommentDto);
        Task<Result<CommentViewDto>> UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task<Result<string>> DeleteCommentAsync(int id);
        Task<Result<IEnumerable<CommentViewDto>>> GetCommentsByProblemId(int problemId);
        Task<Result<string>> LikeComment(int commentId);
        Task<Result<string>> UnLikeComment(int commentId);
        Task<Result<IEnumerable<CommentViewDto>>> SortCommentByLike(int problemId);
        Task<Result<IEnumerable<CommentViewDto>>> SortCommentByCreateAt(int problemId);
    }
}
