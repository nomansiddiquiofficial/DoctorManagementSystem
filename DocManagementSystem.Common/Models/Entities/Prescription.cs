using System;
using System.ComponentModel.DataAnnotations;

namespace DocManagementSystem.Common.Models.Entities
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        public string? DoctorName { get; set; }

        public string? Specialization { get; set; }

        public string? RegNo { get; set; }

        public string? Contact { get; set; }

        public string? PatientName { get; set; }

        public int? PatientAge { get; set; }

        public string? PatientGender { get; set; }

        // For canvas output (e.g., save as base64 string or file URL)
        public string? PrescriptionImage { get; set; }  // Base64 or URL to saved PNG/PDF

        // Optional: Parsed fields if needed
        public string? Medication { get; set; }

        public string? Dosage { get; set; }

        public string? Frequency { get; set; }

        public string? Duration { get; set; }
    }
}