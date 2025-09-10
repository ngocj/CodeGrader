using Application.Dtos.Resquest;
using Application.ExternalServices.Interface;
using Application.Services.Interface;
using Common.ResultPattern;
using Domain.Entities;
using Infrastructure.UnitOfWorks;

namespace Application.Services.Implementation
{
    public class UserProgressService : IUserProgressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProblemExternalService _problemExternalService;

        public UserProgressService(IUnitOfWork unitOfWork, IProblemExternalService problemExternalService)
        {
            _unitOfWork = unitOfWork;
            _problemExternalService = problemExternalService;
        }

        public async Task<Result<GradedResult>> AddUserProgressAsync(GradedResult gradedResult)
        {
            var userProgress = await _unitOfWork.UserProgress.GetById(gradedResult.UserId);
            var problemResult = await _problemExternalService.GetProblemById(gradedResult.ProblemId);
            var userProgressEntity = userProgress.Data;

            if (!userProgress.IsSuccess || userProgress.Data == null)
            {
                var newUserProgress = new UserProgress
                {
                    Id = gradedResult.UserId,
                    TotalSubmisstion = 1,
                    EasySolved = 0,
                    MediumSolved = 0,
                    HardSolved = 0,
                    Rank = 0

                };
                switch (problemResult.Data.Level)
                {
                    case 1: newUserProgress.EasySolved++; break;
                    case 2: newUserProgress.MediumSolved++; break;
                    case 3: newUserProgress.HardSolved++; break;
                }

                newUserProgress.Rank =
                    newUserProgress.EasySolved * 1 +
                    newUserProgress.MediumSolved * 2 +
                    newUserProgress.HardSolved * 3;

                await _unitOfWork.UserProgress.AddAsync(newUserProgress);
                await _unitOfWork.SaveChangesAsync();
                return Result<GradedResult>.Success("Add user progress successfully", null);
            }          
      
            userProgressEntity.TotalSubmisstion += 1;
           

            if (problemResult.Data == null)
            {
                return Result<GradedResult>.Failure("Problem not found");
            }

            switch (problemResult.Data.Level)
            {
                case 1: userProgressEntity.EasySolved++; break;
                case 2: userProgressEntity.MediumSolved++; break;
                case 3: userProgressEntity.HardSolved++; break;
            }

            userProgressEntity.Rank =
                userProgressEntity.EasySolved * 1 +
                userProgressEntity.MediumSolved * 2 +
                userProgressEntity.HardSolved * 3;

            try
            {
                _unitOfWork.UserProgress.UpdateAsync(userProgressEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Result<GradedResult>.Failure("Add user progress failed");
            }

            return Result<GradedResult>.Success("Add user progress successfully", null);
        }

        public async Task<Result<UserProgress>> GetUserProgress(int userId)
            {
            var result = await _unitOfWork.UserProgress.GetById(userId);
            if(!result.IsSuccess)
            {
                return Result<UserProgress>.Failure("User id not found");
            }
            return Result<UserProgress>.Success(result.Data);
        }

        public async Task<Result<List<UserProgress>>> GetUserProgress()
        {
            var result = await _unitOfWork.UserProgress.GetAllAsync();
            return Result<List<UserProgress>>.Success(result.Data);
        }
    }
}
