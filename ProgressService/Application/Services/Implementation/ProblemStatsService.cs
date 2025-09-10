using Application.Dtos.ProblemStatsDtos;
using Application.Dtos.Resquest;
using Application.Services.Interface;
using AutoMapper;
using Common.ResultPattern;
using Domain.Entities;
using Infrastructure.UnitOfWorks;

namespace Application.Services.Implementation
{
    public class ProblemStatsService : IProblemStatsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProblemStatsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ProblemStatsIndexDto>>> GetAllProblemStatsAync()
        {
            var result = await _unitOfWork.ProblemStats.GetAllAsync();
            return Result<List<ProblemStatsIndexDto>>.Success(_mapper.Map<List<ProblemStatsIndexDto>>(result.Data));
        }

        public async Task<Result<ProblemStatsIndexDto>> GetProblemStatsByIdAync(int problemId)
        {
            var result = await _unitOfWork.ProblemStats.GetById(problemId);
            if (!result.IsSuccess)
            {
                return Result<ProblemStatsIndexDto>.Failure("Problem id not found");
            }
            return Result<ProblemStatsIndexDto>.Success(_mapper.Map<ProblemStatsIndexDto>(result.Data));
        }

        public async Task<Result> AddProblemStatAsync(GradedResult gradedResult)
        {
            var problemStat = await _unitOfWork.ProblemStats.GetById(gradedResult.ProblemId);
            if (!problemStat.IsSuccess || problemStat.Data == null)
            {
               await _unitOfWork.ProblemStats.AddAsync( new ProblemStats
               {
                   Id = gradedResult.ProblemId,
                   TotalSubmisstion = 1,
                   AvgPoint = gradedResult.Point
               });
                await _unitOfWork.SaveChangesAsync();    
                return Result.Success("Add problem stat successfully");
            }

             var existingProblemStat = problemStat.Data;
             existingProblemStat.TotalSubmisstion += 1;
             existingProblemStat.AvgPoint = 
                (existingProblemStat.AvgPoint * (existingProblemStat.TotalSubmisstion - 1) + gradedResult.Point) 
                / existingProblemStat.TotalSubmisstion;

            try
            {
                _unitOfWork.ProblemStats.UpdateAsync(existingProblemStat);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Result.Failure("Add problem stat failed");
            }
            return Result.Success("Add problem stat successfully");
        }  
    }
}
