using Application.Common.Mappings;
using AutoMapper;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostRequest : IMapFrom<UpdatePostCommand>
    {
        public string Title { get; set; }
        
        public string Body { get; set; }
        public int Id { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePostCommand, UpdatePostRequest>();
        }
        
    }
}