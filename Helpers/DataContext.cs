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
