using System.ComponentModel.DataAnnotations;

namespace COMP306402_ProjectDemo.DTO
{
    public class ProgramUpdateDTO
    {
        [Required]
        public int ProgramId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int DurationMonths { get; set; }

        public decimal TuitionFee { get; set; }
    }
}
