
using System.Text.Json.Serialization;

namespace DocManagementSystem.Common.Models.Api.Request
{
    public class AddDepartmentRequest
    {
        public string DepartmentName { get; set; }
        public int HeadDoctorId { get; set; }

    }
}
