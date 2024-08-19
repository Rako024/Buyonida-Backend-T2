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
    public class PersonalConfiguration : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> builder)
        {
            builder.Property(x => x.NameOnCard).IsRequired().HasMaxLength(128);
            builder.Property(x => x.CardNumber).IsRequired().HasMaxLength(16);


            builder.HasCheckConstraint("CK_CardNumber_Length", "LEN(CardNumber) = 16");
        }
    }
}
