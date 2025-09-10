using Application.Dtos.CommentDto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment,CreateCommentDto>().ReverseMap();
            CreateMap<Comment, CommentViewDto>().ReverseMap();
            CreateMap<Comment, UpdateCommentDto>().ReverseMap();
        }
    }
}
