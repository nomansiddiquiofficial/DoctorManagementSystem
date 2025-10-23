
using DoctorManagementSystem.Common.Entities.Models.Entities;
using System.Text.Json.Serialization;
using static DoctorManagementSystem.Common.Entities.Models.Entities.DoctorVM;

namespace DocManagementSystem.Common.Models.Api.Request
{
    public class EditDoctorRequest
    {
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialty { get; set; }
        public string? Department { get; set; }
       public string? DocStatus { get; set; }
    }
}
