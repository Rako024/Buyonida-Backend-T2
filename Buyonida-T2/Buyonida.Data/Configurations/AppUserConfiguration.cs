using Buyonida.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(40);
            builder.Property(x => x.Surname).HasMaxLength(50);
            builder.Property(x => x.IDNumber).HasMaxLength(30);
            builder.Property(x => x.MobilNumber).HasMaxLength(20);
            builder.Property(x => x.Adress).HasMaxLength(150);
            builder.Property(x => x.City).HasMaxLength(60);
            builder.Property(x => x.District).HasMaxLength(60);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
