using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocManagementSystem.Common.Models.Entities
{
    public class DoctorPatient
    {
        public int DoctorId { get; set; }
        public required Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public required Patient Patient { get; set; }

        // Extra fields
        public DateTime AppointmentDate { get; set; }
        public string? Notes { get; set; }
    }
}
