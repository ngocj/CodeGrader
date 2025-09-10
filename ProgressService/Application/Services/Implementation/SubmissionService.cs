using Application.Dtos.Resquest;
using Application.Dtos.SubmisstionDtos;
using Application.Services.Interface;
using AutoMapper;
using Common.ResultPattern;
using Domain.Entities;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Http;
namespace Application.Services.Implementation
{
    public class SubmissionService : ISubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;    
        private readonly IMapper _mapper;
      
        public SubmissionService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
          
        }

        public async Task<Result<SubmisstionDetailDto>> AddSubmission(GradedResult gradedResult)
        {

            var submisstionEntity = _mapper.Map<Submission>(gradedResult);
            try
            {
                await _unitOfWork.Submission.AddAsync(submisstionEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Result<SubmisstionDetailDto>.Failure("Add submission failed");
            }

            return Result<SubmisstionDetailDto>.Success("Add submission successfully",null);
        }

        public async Task<Result<string>> DeleteSubmisstion(int submissionId)
        {
            var submmissionExists =  _unitOfWork.Submission.GetById(submissionId).Result.Data;
            if (submmissionExists == null)
    {
                return Result<string>.Failure("Submission not found");
            }
            _unitOfWork.Submission.Remove(submmissionExists);
            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success("Delete submission successfully", null);
        }
        
        public async Task<Result<List<SubmisstionDetailDto>>> GetAllSubmission()
        {
            var result =  await _unitOfWork.Submission.GetAllAsync();

            var resultDto = _mapper.Map<List<SubmisstionDetailDto>>(result.Data);
        
            return Result<List<SubmisstionDetailDto>>.Success("Get all Submisstion successfully", resultDto);
        }

        public async Task<Result<SubmisstionDetailDto>> GetSubmisstion(int submissionId)
        {
            var submmissionExists = await _unitOfWork.Submission.GetById(submissionId);   

            if (submmissionExists == null)
        {
                return Result<SubmisstionDetailDto>.Failure("Submission not found");
        }

            return Result<SubmisstionDetailDto>.Success("Get submission successfully", _mapper.Map<SubmisstionDetailDto>(submmissionExists.Data));                  
        }

        public async Task<Result<List<SubmissionIndexDto>>> GetSubmisstionsByProblemId(int ProblemId)
        {
      
            var result = await _unitOfWork.Submission.GetByProblemId(ProblemId);     

             return Result<List<SubmissionIndexDto>>.Success("Get submissions successfully", _mapper.Map<List<SubmissionIndexDto>>(result));
        }

        public async Task<Result<List<SubmissionIndexDto>>> GetSubmisstionsByUserId()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("id")?.Value);

            var result = await _unitOfWork.Submission.GetByUserId(userId);

            return Result<List<SubmissionIndexDto>>.Success("Get submissions successfully", _mapper.Map<List<SubmissionIndexDto>>(result));
        }
    }
}
