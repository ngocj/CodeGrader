using Application.Dtos.ProblemStatsDtos;
using Application.Dtos.Resquest;
using Application.Dtos.SubmisstionDtos;
using AutoMapper;
using Domain.Entities;
using Domain.ValueEntities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GradedResult, Submission>().ReverseMap();

            CreateMap<Submission, SubmissionIndexDto>().ReverseMap();

            CreateMap<EvaluationCriteriaDto, EvaluationCriteria>().ReverseMap();

            CreateMap<Submission, SubmisstionDetailDto>().ReverseMap();

            CreateMap<ProblemStats, ProblemStatsIndexDto>().ReverseMap();
        }
    }
}
