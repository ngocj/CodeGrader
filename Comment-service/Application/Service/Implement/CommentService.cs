using Application.Dtos.CommentDto;
using Application.Service.Interface;
using AutoMapper;
using Common;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Application.Service.Implement
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor  _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient.CreateClient("ProblemService");
        }
        public async Task<Result<CommentViewDto>> AddCommentAsync(CreateCommentDto createCommentDto)
        {
            var errors = new List<ErrorField>();

            var userClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            if (string.IsNullOrWhiteSpace(userClaim))
                return Result<CommentViewDto>.Failure("Invalid token");

            var userId = int.Parse(userClaim);

            if (string.IsNullOrWhiteSpace(createCommentDto.CommentText))
                errors.Add(new ErrorField { Field = "CommentText", errorMessage = "CommentText is required" });

            var problemResponse = await _httpClient.GetAsync($"/problem/{createCommentDto.ProblemId}"); 

            var apiResult = await problemResponse.Content.ReadFromJsonAsync<Result<Object>>();
            if (!apiResult.IsSuccess)
            {
                errors.Add(new ErrorField { Field = "ProblemId", errorMessage = "Problem id not found" });
            }             
                Comment parent = null;
            if (createCommentDto.ParentCommentId.HasValue)
            {
                parent = await _unitOfWork.CommentRepository.GetById(createCommentDto.ParentCommentId.Value);
                if (parent == null)
                    errors.Add(new ErrorField { Field = "ParentCommentId", errorMessage = "ParenComment id not found" });            
            }

            if (errors.Any())
                return Result<CommentViewDto>.Failure(errors);

            var commentEntity = _mapper.Map<Comment>(createCommentDto);
            commentEntity.UserId = userId;
            commentEntity.CreatedAt = DateTime.UtcNow;

            try
            {
                await _unitOfWork.CommentRepository.AddAsync(commentEntity);
                await _unitOfWork.SaveChangeAsync();
            }
            catch
            {
                return Result<CommentViewDto>.Failure("Add comment failed");
            }            
            return Result<CommentViewDto>.Success(null, "Add comment successfully");
        }
        public async Task<Result<string>> DeleteCommentAsync(int id)
        {
            var userClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            var roleClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (!int.TryParse(userClaim, out var userId))
                return Result<string>.Failure("Invalid token");

            var comment = await _unitOfWork.CommentRepository.GetById(id);
            if (comment == null)
                return Result<string>.Failure("Comment id not found");

            if (userId != comment.UserId && roleClaim != "Admin")
                return Result<string>.Failure("You are not allowed to delete this comment");

            // xoa comment con
            foreach (var reply in comment.Replies)
            {
                await _unitOfWork.CommentRepository.DeleteAsync(reply);
            }
            try
            {
                await _unitOfWork.CommentRepository.DeleteAsync(comment);
                await _unitOfWork.SaveChangeAsync();
                return Result<string>.Success(null, "Delete comment successfully");
            }
            catch
            {
                return Result<string>.Failure("Delete comment failed");
            }
        }
        public async Task<Result<IEnumerable<CommentViewDto>>> GetAllCommentAsync()
        {
           var comment = await _unitOfWork.CommentRepository.GetAllAsync();

           var commentViewDto = _mapper.Map<IEnumerable<CommentViewDto>>(comment);

           return Result<IEnumerable<CommentViewDto>>.Success(commentViewDto, "Get all comment successfully");      
        }
        public async Task<Result<CommentViewDto>> GetCommentById(int id)
        {
           
            var comment = await _unitOfWork.CommentRepository.GetById(id);
            if(comment == null)
            {
                return Result<CommentViewDto>.Failure("Comment id not found");
            }
            var commentViewDto = _mapper.Map<CommentViewDto>(comment);

            return Result<CommentViewDto>.Success(commentViewDto, "Get comment by id successfully");
        }
        public async Task<Result<IEnumerable<CommentViewDto>>> GetCommentsByProblemId(int problemId)
        {
            // check problem id
            var problemResponse = await _httpClient.GetAsync($"/problem/{problemId}"); 
            var apiResult = await problemResponse.Content.ReadFromJsonAsync<Result<object>>();
            if (!apiResult.IsSuccess)
            {
                return Result<IEnumerable<CommentViewDto>>.Failure("Problem id not found");
            }
            var comments = await _unitOfWork.CommentRepository.GetCommentByProblemId(problemId);
            var commentViewDtos = _mapper.Map<IEnumerable<CommentViewDto>>(comments);
            return Result<IEnumerable<CommentViewDto>>.Success(commentViewDtos, "Get comments by problem id successfully");
        }
        public async Task<Result<string>> LikeComment(int commentId)
        {
            var userClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            if (string.IsNullOrWhiteSpace(userClaim))
                return Result<string>.Failure("Invalid token");
            var userId = int.Parse(userClaim);
            var comment = await _unitOfWork.CommentRepository.GetById(commentId);
            if (comment == null)
                return Result<string>.Failure("Comment id not found");
            comment.Like += 1;
            try
            {
                await _unitOfWork.CommentRepository.UpdateAsync(comment);
                await _unitOfWork.SaveChangeAsync();
                return Result<string>.Success(null, "Like comment successfully");
            }
            catch
            {
                return Result<string>.Failure("Like comment failed");
            }
        }
        public async Task<Result<IEnumerable<CommentViewDto>>> SortCommentByCreateAt(int problemId)
        {
            var result = await _unitOfWork.CommentRepository.SortCommentByCreatedAt(problemId);
            var resultDto = _mapper.Map<IEnumerable<CommentViewDto>>(result);
            return Result<IEnumerable<CommentViewDto>>.Success(resultDto, "Sort comment by create at successfully");
        }
        public async Task<Result<IEnumerable<CommentViewDto>>> SortCommentByLike(int problemId)
        {

            var result = await _unitOfWork.CommentRepository.SortCommentByLike(problemId);

            var resultDto = _mapper.Map<IEnumerable<CommentViewDto>>(result);

            return Result<IEnumerable<CommentViewDto>>.Success(resultDto, "Sort comment by like successfully");
        }
        public async Task<Result<string>> UnLikeComment(int commentId)
        {
            var userClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            if (string.IsNullOrWhiteSpace(userClaim))
                return Result<string>.Failure("Invalid token");
            var userId = int.Parse(userClaim);
            var comment = await _unitOfWork.CommentRepository.GetById(commentId);
            if (comment == null)
                return Result<string>.Failure("Comment id not found");
            comment.Like -= 1;
            try
            {
                await _unitOfWork.CommentRepository.UpdateAsync(comment);
                await _unitOfWork.SaveChangeAsync();
                return Result<string>.Success(null, "UnLike comment successfully");
            }
            catch
            {
                return Result<string>.Failure("UnLike comment failed");
            }
        }
        public async Task<Result<CommentViewDto>> UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            var errors = new List<ErrorField>();

            var userClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            if (string.IsNullOrWhiteSpace(userClaim))
                return Result<CommentViewDto>.Failure("Invalid token");

            var userId = int.Parse(userClaim);
    
            // Lấy comment hiện tại
            var commentEntity = await _unitOfWork.CommentRepository.GetById(updateCommentDto.Id);
            if (commentEntity == null)
                return Result<CommentViewDto>.Failure("Comment id not found");

            // check quyen sua comment
            if (commentEntity.UserId != userId)
            {
                return Result<CommentViewDto>.Failure("You not allow update this comment");
            }

            // Validate input
            if (string.IsNullOrWhiteSpace(updateCommentDto.CommentText))
                errors.Add(new ErrorField { Field = "CommentText", errorMessage = "CommentText is required" });
                  
            if (errors.Any())
                return Result<CommentViewDto>.Failure(errors);
            
            commentEntity.CommentText = updateCommentDto.CommentText;     
            try
            {
                await _unitOfWork.CommentRepository.UpdateAsync(commentEntity);
                await _unitOfWork.SaveChangeAsync();
            }
            catch
            {
                return Result<CommentViewDto>.Failure("Update comment failed");
            }

            var commentViewDto = _mapper.Map<CommentViewDto>(commentEntity);
            return Result<CommentViewDto>.Success(commentViewDto, "Update comment successfully");
        }
    }
}
