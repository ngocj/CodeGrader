using Application.Dtos.Resquest;
using Application.Dtos.SubmisstionDtos;
using Common.ResultPattern;

namespace Application.Services.Interface
{
    public interface ISubmissionService
    {
        public Task<Result<SubmisstionDetailDto>> GetSubmisstion(int submissionId);
        public Task<Result<List<SubmissionIndexDto>>> GetSubmisstionsByUserId();
        public Task<Result<List<SubmisstionDetailDto>>> GetAllSubmission();
        public Task<Result<List<SubmissionIndexDto>>> GetSubmisstionsByProblemId(int problemId);
        public Task<Result<SubmisstionDetailDto>> AddSubmission(GradedResult gradedResult);
        public Task<Result<string>> DeleteSubmisstion(int submissionId);

    }
}
