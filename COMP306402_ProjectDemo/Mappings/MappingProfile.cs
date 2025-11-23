using AutoMapper;
using COMP306402_ProjectDemo.DTO;
using COMP306402_ProjectDemo.Models;

namespace COMP306402_ProjectDemo.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Student Mappings
            CreateMap<Student, StudentReadDTO>();
            CreateMap<StudentCreateDTO, Student>();
            CreateMap<StudentUpdateDTO, Student>();

            // Program Mappings
            CreateMap<AcademicProgram, ProgramReadDTO>();
            CreateMap<ProgramCreateDTO, AcademicProgram>();
            CreateMap<ProgramUpdateDTO, AcademicProgram>();

            // Enrollment Mappings
            CreateMap<Enrollment, EnrollmentReadDTO>();
            CreateMap<EnrollmentCreateDTO, Enrollment>();
            CreateMap<EnrollmentUpdateDTO, Enrollment>();
        }
    }
}
