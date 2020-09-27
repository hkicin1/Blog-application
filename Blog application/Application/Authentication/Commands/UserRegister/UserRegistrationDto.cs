using Application.Common.Mappings;
using AutoMapper;

namespace Application.Authentication.Commands.UserRegister
{
    public class UserRegistrationDto : IMapFrom<UserRegistrationCommand>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRegistrationCommand, UserRegistrationDto>();
        }
    }
}
