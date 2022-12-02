using AutoMapper;

namespace SchollOfDevs.Profiles
{
    public static class MapperConfig
    {
        public static MapperConfiguration GetMapperConfig()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new NoteProfile());
                mc.AddProfile(new CourseProfile());
            });
        }
    }
}
