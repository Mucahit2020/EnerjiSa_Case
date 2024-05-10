using EnergyOutageNotifier.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Models.Context
{
    public class EnerjisaDBContext : DbContext
    {
        public EnerjisaDBContext(DbContextOptions<EnerjisaDBContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=MONSTER;Database=EnerjisaDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var model in modelBuilder.Model.GetEntityTypes())
            {
                model.FindProperty("NotificationTime")?.SetDefaultValueSql("getdate()");
            }
        }
        public DbSet<Outage> Outage { get; set; }
        public DbSet<Notification> Notification { get; set; }



    }


}
