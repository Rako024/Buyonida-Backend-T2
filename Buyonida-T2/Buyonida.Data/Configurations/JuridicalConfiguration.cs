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
    public class JuridicalConfiguration : IEntityTypeConfiguration<Juridical>
    {
        public void Configure(EntityTypeBuilder<Juridical> builder)
        {
            builder.Property(x => x.Voen).IsRequired().HasMaxLength(100);
            builder.Property(x => x.BankName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.BankVoen).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Mh).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SwiftBik).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Hh).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Director).IsRequired().HasMaxLength(100);
        }
    }
}
