using AutoMapper;
using SchollOfDevs.Dto.User;
using SchollOfDevs.Entities;

namespace SchollOfDevs.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRequest>();
            CreateMap<User, UserResponse>();
            CreateMap<User, UserRequestUpdate>();
        }
    }
}
