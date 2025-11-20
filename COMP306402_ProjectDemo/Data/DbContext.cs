using Microsoft.EntityFrameworkCore;
using COMP306402_ProjectDemo.Models;

namespace COMP306402_ProjectDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<AcademicProgram> AcedemicPrograms { get; set; } 
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --------------------------------------------------------
            // 1. Program Data (3 Records)
            // --------------------------------------------------------
            modelBuilder.Entity<AcademicProgram>().HasData(
                new AcademicProgram
                {
                    ProgramId = 1,
                    Name = "Software Engineering",
                    Description = "Comprehensive software development curriculum.",
                    DurationMonths = 12,
                    TuitionFee = 15000.00m
                },
                new AcademicProgram
                {
                    ProgramId = 2,
                    Name = "Computer Networking",
                    Description = "Focus on network infrastructure and security.",
                    DurationMonths = 8,
                    TuitionFee = 12000.00m
                },
                new AcademicProgram
                {
                    ProgramId = 3,
                    Name = "Graphic Design Fundamentals",
                    Description = "Creative and technical skills for digital media.",
                    DurationMonths = 6,
                    TuitionFee = 8500.00m
                }
            );

            // --------------------------------------------------------
            // 2. Student Data (10 Records)
            // --------------------------------------------------------
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, FirstName = "John", LastName = "Doe", EnrollmentDate = new DateTime(2024, 09, 01) },
                new Student { StudentId = 2, FirstName = "Jane", LastName = "Smith", EnrollmentDate = new DateTime(2024, 09, 01) },
                new Student { StudentId = 3, FirstName = "Ali", LastName = "Khan", EnrollmentDate = new DateTime(2024, 05, 15) },
                new Student { StudentId = 4, FirstName = "Maria", LastName = "Garcia", EnrollmentDate = new DateTime(2023, 01, 10) },
                new Student { StudentId = 5, FirstName = "David", LastName = "Lee", EnrollmentDate = new DateTime(2024, 09, 01) },
                new Student { StudentId = 6, FirstName = "Emily", LastName = "Chen", EnrollmentDate = new DateTime(2023, 09, 01) },
                new Student { StudentId = 7, FirstName = "Mark", LastName = "Wilson", EnrollmentDate = new DateTime(2024, 01, 20) },
                new Student { StudentId = 8, FirstName = "Sarah", LastName = "Brown", EnrollmentDate = new DateTime(2024, 05, 15) },
                new Student { StudentId = 9, FirstName = "Kevin", LastName = "Black", EnrollmentDate = new DateTime(2023, 09, 01) },
                new Student { StudentId = 10, FirstName = "Lisa", LastName = "White", EnrollmentDate = new DateTime(2023, 01, 10) }
            );

            // --------------------------------------------------------
            // 3. Enrollment Data (12 Records - Must be > 10)
            // --------------------------------------------------------
            modelBuilder.Entity<Enrollment>().HasData(
                // 10 Active/Completed Enrollments for requirement fulfillment
                new Enrollment { EnrollmentId = 1, StudentId = 1, ProgramId = 1, EnrollmentDate = new DateTime(2024, 09, 01), Status = "Active" },
                new Enrollment { EnrollmentId = 2, StudentId = 2, ProgramId = 1, EnrollmentDate = new DateTime(2024, 09, 01), Status = "Active" },
                new Enrollment { EnrollmentId = 3, StudentId = 3, ProgramId = 2, EnrollmentDate = new DateTime(2024, 05, 15), Status = "Active" },
                new Enrollment { EnrollmentId = 4, StudentId = 4, ProgramId = 2, EnrollmentDate = new DateTime(2023, 01, 10), Status = "Completed" },
                new Enrollment { EnrollmentId = 5, StudentId = 5, ProgramId = 3, EnrollmentDate = new DateTime(2024, 09, 01), Status = "Deferred" },
                new Enrollment { EnrollmentId = 6, StudentId = 6, ProgramId = 1, EnrollmentDate = new DateTime(2023, 09, 01), Status = "Active" },
                new Enrollment { EnrollmentId = 7, StudentId = 7, ProgramId = 2, EnrollmentDate = new DateTime(2024, 01, 20), Status = "Active" },
                new Enrollment { EnrollmentId = 8, StudentId = 8, ProgramId = 3, EnrollmentDate = new DateTime(2024, 05, 15), Status = "Active" },
                new Enrollment { EnrollmentId = 9, StudentId = 9, ProgramId = 1, EnrollmentDate = new DateTime(2023, 09, 01), Status = "Completed" },
                new Enrollment { EnrollmentId = 10, StudentId = 10, ProgramId = 2, EnrollmentDate = new DateTime(2023, 01, 10), Status = "Completed" },
                // Extra data
                new Enrollment { EnrollmentId = 11, StudentId = 1, ProgramId = 3, EnrollmentDate = new DateTime(2023, 01, 10), Status = "Completed" },
                new Enrollment { EnrollmentId = 12, StudentId = 5, ProgramId = 1, EnrollmentDate = new DateTime(2023, 05, 10), Status = "Active" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}