
using DoctorManagementSystem.Common.Entities.Models.Entities;
using System.Text.Json.Serialization;

namespace DocManagementSystem.Common.Models.Api.Request
{

    public class AddPaitentRequest
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int DoctorId { get; set; }
        public DoctorVM Doctor { get; set; }
        
    }

}
