using DoctorManagementSystem.Common.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DocManagementSystem.Common.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Tables
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }

    }
}