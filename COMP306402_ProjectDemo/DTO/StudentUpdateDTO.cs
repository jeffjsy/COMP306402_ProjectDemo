using System.ComponentModel.DataAnnotations;

namespace COMP306402_ProjectDemo.DTO
{
    public class StudentUpdateDTO
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}
