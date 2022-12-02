using SchollOfDevs.Enuns;

namespace SchollOfDevs.Dto.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public TypeUser TypeUser { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpadateAt { get; set; }
        public List<Entities.Course> CoursesStuding { get; set; } // typeUser == Student
        public List<Entities.Course> CoursesTeaching { get; set; } // typer == Teacher
    }
}
