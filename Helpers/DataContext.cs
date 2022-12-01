using Microsoft.EntityFrameworkCore;
using SchollOfDevs.Entities;
using SchollOfDevs.Enuns;

namespace SchollOfDevs.Helpers
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .Property(e => e.TypeUser)
                .HasConversion(
                    v => v.ToString(),
                    v => (TypeUser)Enum.Parse(typeof(TypeUser), v));

            builder.Entity<Course>()
                .HasOne(e => e.Teacher)
                .WithMany(c => c.CoursesTeaching)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .Entity<Course>()
                .HasMany(p => p.Students)
                .WithMany(p => p.CoursesStuding)
                .UsingEntity<StudentCourse>(
                    j => j
                        .HasOne(pt => pt.Student)
                        .WithMany(t => t.StudentCourses)
                        .HasForeignKey(pt => pt.StudentId),
                    j => j
                        .HasOne(pt => pt.Course)
                        .WithMany(t => t.StudentCourses)
                        .HasForeignKey(pt => pt.CourseId),
                    j =>
                    {
                        j.HasKey(t => new { t.CourseId, t.StudentId });
                    });
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added 
                    || e.State == EntityState.Modified
                    )
                );

            foreach(var entityEntry in entries)
            {
                DateTime datetime = DateTime.Now;
                ((BaseEntity)entityEntry.Entity).UpadateAt = datetime;

                if(entityEntry.State == EntityState.Added)
                    ((BaseEntity)entityEntry.Entity).CreateAt = datetime;
            }
            
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
