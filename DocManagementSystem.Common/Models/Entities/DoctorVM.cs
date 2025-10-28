using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoctorManagementSystem.Common.Entities.Models.Entities
{
    public class DoctorVM
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Specialty { get; set; }

        [StringLength(100)]
        public string Department { get; set; }

        [StringLength(100)]
        public string Gender { get; set; }

        public Status DocStatus { get; set; } = Status.Active;

        [Phone, StringLength(20)]
        public string PhoneNumber { get; set; }

        // Navigation Property - One Doctor can have many Patients
        public List<PatientVM> Patients { get; set; } = new List<PatientVM>();

        public enum Status
        {
            Inactive = 0,
            Active = 1
        }

        // Foreign Key - One Department per Doctor
        public int? DepartmentId { get; set; }

        // Navigation Property - One Doctor belongs to one Department
        public DepartmentVM? DepartmentVM { get; set; }
    }
}
