using System.ComponentModel.DataAnnotations;

namespace DoctorManagementSystem.Common.Entities.Models.Entities
{
    public class PatientVM
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        public int Age { get; set; }

        [StringLength(100)]
        public string Gender { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Phone, StringLength(20)]
        public string PhoneNumber { get; set; }

        // Foreign Key
        public int DoctorId { get; set; }

        // Navigation Property - Each patient has one doctor
        public DoctorVM Doctor { get; set; }
    }
}
