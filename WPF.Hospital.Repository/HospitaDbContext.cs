using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Hospital.Repository
{
    public class HospitaDbContext : DbContext
    {
        public HospitaDbContext(DbContextOptions<HospitaDbContext> options) : base(options)
        {
        }

        public DbSet<Model.Patients> Patients { get; set; }
        public DbSet<Model.Doctor> Doctor { get; set; }
        public DbSet<Model.Medicine> Medicine { get; set; }
        public DbSet<Model.History> History { get; set; }
        public DbSet<Model.Prescription> Prescriptions { get; set; }
    }
}
