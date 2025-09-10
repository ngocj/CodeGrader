using Application.Dtos.Resquest;
using Common.ResultPattern;
using Domain.Entities;

namespace Application.Services.Interface
{
    public interface IUserProgressService
    {
        Task<Result<UserProgress>> GetUserProgress(int userId);
        Task<Result<List<UserProgress>>> GetUserProgress();
        Task<Result<GradedResult>> AddUserProgressAsync(GradedResult gradedResult);       
    }
}
