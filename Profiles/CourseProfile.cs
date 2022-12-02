using AutoMapper;
using SchollOfDevs.Dto.Course;
using SchollOfDevs.Entities;

namespace SchollOfDevs.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseRequest>();
            CreateMap<Course, CourseResponse>();

            CreateMap<CourseRequest, Course>();
            CreateMap<CourseResponse, Course>();
        }
    }
}
