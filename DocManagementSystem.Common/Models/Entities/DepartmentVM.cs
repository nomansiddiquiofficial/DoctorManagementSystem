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

        public DoctorVM Doctor { get; set; }

    }
}
