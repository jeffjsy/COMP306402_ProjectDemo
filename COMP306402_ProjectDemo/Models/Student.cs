namespace COMP306402_ProjectDemo.Models
{
    public class Student
    {
        public int StudentId { get; set; } // Primary Key (PK)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Navigation property for the one-to-many relationship
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
