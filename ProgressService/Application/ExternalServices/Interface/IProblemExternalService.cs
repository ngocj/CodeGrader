using Application.Dtos.ExternalDtos;
using Common.ResultPattern;

namespace Application.ExternalServices.Interface
{
    public interface IProblemExternalService
    {
        Task<Result<ProblemDto>> GetProblemById(int problemId);
    }
}
