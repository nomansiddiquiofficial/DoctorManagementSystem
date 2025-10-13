using DoctorManagementSystem.Common.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoctorManagementSystem.Common.Entities.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Specialty { get; set; }
        
        [StringLength(100)]
        public string Department{ get; set; }

        public int Paitents{ get; set; }
        public Status DocStatus { get; set; } =  Status.Active;

        [Phone, StringLength(20)]
        public string Phone { get; set; }

        [EmailAddress, StringLength(100)]
        public string Email { get; set; }

        public ICollection<MedicalRecord> MedicalRecords { get; set; }

        public enum Status
        {
            Inactive = 0,
            Active = 1
        }

    }
}
