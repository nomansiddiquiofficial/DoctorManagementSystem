using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoctorManagementSystem.Common.Entities.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [Phone, StringLength(20)]
        public string Phone { get; set; }

        public ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}
