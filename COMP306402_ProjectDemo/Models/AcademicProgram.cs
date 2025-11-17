using System.ComponentModel.DataAnnotations;

namespace COMP306402_ProjectDemo.Models
{
    public class AcademicProgram
    {
        [Key]
        public int ProgramId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int DurationMonths { get; set; }

        public decimal TuitionFee { get; set; }

        // Navigation property for the one-to-many relationship with Enrollments
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
