using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocManagementSystem.Common.Models.Entities
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public required string FullName { get; set; }

        [StringLength(15)]
        public string? ContactNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Specialization { get; set; }

        public ICollection<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}