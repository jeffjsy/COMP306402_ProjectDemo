namespace COMP306402_ProjectDemo.DTO
{
    public class EnrollmentReadDTO
    {
        public int EnrollmentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }

        public int StudentId { get; set; }
        public int ProgramId { get; set; }
    }
}
