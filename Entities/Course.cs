using System.Text.Json.Serialization;

namespace SchollOfDevs.Entities
{
    public class Course: BaseEntity
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual User Teacher { get; set; }
        [JsonIgnore]
        public ICollection<User> Students { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
