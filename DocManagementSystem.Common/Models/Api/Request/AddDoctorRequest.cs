
using System.Text.Json.Serialization;

namespace DocManagementSystem.Common.Models.Api.Request
{
    public class AddDoctorRequest
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialty { get; set; }
        public int DepartmentId { get; set; }
    }

}
