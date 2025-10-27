using DocManagementSystem.Common.Models.Api.Request;
using DoctorManagementSystem.Common.Entities.Models;
using DoctorManagementSystem.Common.Entities.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocManagementSystem.Common.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Tables
        public DbSet<DoctorVM> Doctors { get; set; }
        public DbSet<PatientVM> Paitents { get; set; }

        public DbSet<DepartmentVM> Departments { get; set; }

    }
}