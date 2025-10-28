using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoctorManagementSystem.Common.Entities.Models.Entities
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string HeadDoctorName { get; set; }
        public int StaffCount { get; set; } = 0;

        public int DoctorId { get; set; }

        // Navigation Property - One Department can have many Doctors
        public List<DoctorVM> Doctors { get; set; } = new List<DoctorVM>();
    }
}
