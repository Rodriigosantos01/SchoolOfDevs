namespace SchollOfDevs.Dto.Course
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Entities.User Teacher { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpadateAt { get; set; }
        public ICollection<Entities.User> Students { get; set; }
    }
}
