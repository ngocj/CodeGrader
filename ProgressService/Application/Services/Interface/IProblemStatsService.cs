using Application.Dtos.ProblemStatsDtos;
using Application.Dtos.Resquest;
using Common.ResultPattern;

namespace Application.Services.Interface
{
    public interface IProblemStatsService
    {
        public Task<Result<List<ProblemStatsIndexDto>>> GetAllProblemStatsAync();
        public Task<Result<ProblemStatsIndexDto>> GetProblemStatsByIdAync(int problemId);
        public Task<Result> AddProblemStatAsync(GradedResult gradedResult);
    }
}
