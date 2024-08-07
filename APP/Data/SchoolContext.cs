using APP.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace APP.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite Key for Enrollment
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            // Relationships for Enrollment
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // to prevent cascading deletes

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // to prevent cascading deletes

            // Relationships for Course
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict); // to prevent cascading deletes

            // Relationships for Student
            modelBuilder.Entity<Student>()
                .HasOne(s => s.School)
                .WithMany(sch => sch.Students)
                .HasForeignKey(s => s.SchoolId)
                .OnDelete(DeleteBehavior.Restrict); // to prevent cascading deletes

            // Relationships for Teacher
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.School)
                .WithMany(sch => sch.Teachers)
                .HasForeignKey(t => t.SchoolId)
                .OnDelete(DeleteBehavior.Restrict); // to prevent cascading deletes

            // Relationships for Grade
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // to prevent cascading deletes

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // to prevent cascading deletes

            // Many-to-Many Relationship for Course and Classroom
            modelBuilder.Entity<CourseClassroom>()
                .HasKey(cc => new { cc.CourseId, cc.ClassroomId });

            modelBuilder.Entity<CourseClassroom>()
                .HasOne(cc => cc.Course)
                .WithMany(c => c.CourseClassrooms)
                .HasForeignKey(cc => cc.CourseId);

            modelBuilder.Entity<CourseClassroom>()
                .HasOne(cc => cc.Classroom)
                .WithMany(c => c.CourseClassrooms)
                .HasForeignKey(cc => cc.ClassroomId);

            // User entity configuration
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            // Additional configuration for required fields and max lengths
            modelBuilder.Entity<School>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.Property(e => e.RoomNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Capacity)
                    .IsRequired();
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.GradeValue)
                    .IsRequired()
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20);
            });

        }
    }
}
