using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace COMP306402_ProjectDemo.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // e.g., "Active", "Completed", "Deferred"

        // --- Foreign Keys (FKs) ---

        // FK to Student table
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        // FK to Program table
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public AcademicProgram AcedemicProgram { get; set; }
    }
}
