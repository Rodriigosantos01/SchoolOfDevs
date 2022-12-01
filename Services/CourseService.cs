﻿using Microsoft.EntityFrameworkCore;
using SchollOfDevs.Entities;
using SchollOfDevs.Helpers;

namespace SchollOfDevs.Services
{
    public interface ICourseService
    {
        public Task<Course> Create(Course course);
        public Task<Course> GetById(int id);
        public Task<List<Course>> GetAll();
        public Task Update(Course courseIn, int id);
        public Task Delete(int id);

    }
    public class CourseService : ICourseService
    {
        private readonly DataContext _context;

        public CourseService(DataContext context)
        {   
               _context = context;
        }
        public async Task<Course> Create(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }

        public async Task Delete(int id)
        {
            Course courseDb = await _context.Courses.SingleOrDefaultAsync(u => u.Id == id);

            if (courseDb is null)
                throw new KeyNotFoundException($"CourseName {id} not found");

            _context.Courses.Remove(courseDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAll() => await _context.Courses.ToListAsync();

        public async Task<Course> GetById(int id)
        {
            Course courseDb = await _context.Courses.SingleOrDefaultAsync(u => u.Id == id);

            if (courseDb is null)
                throw new KeyNotFoundException($"CourseName {id} not found");

            return courseDb;
        }

        public async Task Update(Course courseIn, int id)
        {
            if(courseIn.Id != id)
                throw new BadHttpRequestException("Route id differs Course id");

            Course courseDb = await _context.Courses
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            if (courseDb is null)
                throw new KeyNotFoundException($"CourseName {id} not found");

            courseIn.CreateAt = courseDb.CreateAt;

            _context.Entry(courseIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
