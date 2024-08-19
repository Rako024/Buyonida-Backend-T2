using Buyonida.Core.Entities;
using Buyonida.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Data.DAL
{
    public class BuyonidaContext : IdentityDbContext<AppUser>
    {
        public BuyonidaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Personal> Personals { get; set; }
        public DbSet<Invidual> Inviduals { get; set; }
        public DbSet<Juridical> Juridicals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonalConfiguration());
            modelBuilder.ApplyConfiguration(new InvidualConfiguration());
            modelBuilder.ApplyConfiguration(new JuridicalConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        }
    }
}
