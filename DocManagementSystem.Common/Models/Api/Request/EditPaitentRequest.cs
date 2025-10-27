// EditPatientRequest.cs
namespace DocManagementSystem.Common.Models.Api.Request
{
    public class EditPatientRequest
    {
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int? DoctorId { get; set; }
    }
}