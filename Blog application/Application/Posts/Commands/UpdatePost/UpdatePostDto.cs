using Application.Common.Mappings;
using AutoMapper;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostDto : IMapFrom<UpdatePostResponse>
    {
        public int Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePostResponse, UpdatePostDto>();
        }
    }
}