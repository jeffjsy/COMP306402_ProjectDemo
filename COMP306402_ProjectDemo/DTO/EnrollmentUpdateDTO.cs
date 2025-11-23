using System.ComponentModel.DataAnnotations;

namespace COMP306402_ProjectDemo.DTO
{ 
    public class EnrollmentUpdateDTO
    {
        [Required]
        public int? EnrollmentId { get; set; }

        [Required]
        public DateTime? EnrollmentDate { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public int? StudentId { get; set; }

        [Required]
        public int? ProgramId { get; set; }
    }
}
