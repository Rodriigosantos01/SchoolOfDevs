﻿namespace SchollOfDevs.Dto.Course
{
    public class CourseRequest
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }        
    }
}
