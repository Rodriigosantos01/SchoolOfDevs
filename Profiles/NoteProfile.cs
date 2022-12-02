using AutoMapper;
using SchollOfDevs.Dto.Note;
using SchollOfDevs.Entities;

namespace SchollOfDevs.Profiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteRequest>();
            CreateMap<Note, NoteResponse>();
            
            CreateMap<NoteRequest, Note>();
            CreateMap<NoteResponse, Note>();
        }
    }
}
