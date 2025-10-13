using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorManagementSystem.Common.Entities.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime VisitDate { get; set; } = DateTime.UtcNow;

        public string Diagnosis { get; set; }
        public string Notes { get; set; }
    }
}
