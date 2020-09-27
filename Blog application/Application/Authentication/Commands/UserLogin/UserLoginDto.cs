using Application.Common.Mappings;
using AutoMapper;

namespace Application.Authentication.Commands.UserLogin
{
    public class UserLoginDto : IMapFrom<UserLoginCommand>
    {
        public string Token { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserLoginCommand, UserLoginDto>();
        }
    }
}
