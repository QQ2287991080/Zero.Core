using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;

namespace Zero.Core.EfCore.EntityConfigs
{
    public class DictionariesConfig : IEntityTypeConfiguration<Dictionaries>
    {
        public void Configure(EntityTypeBuilder<Dictionaries> builder)
        {
            builder.SetEntityConfig();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Memo).HasMaxLength(1000);
            builder.Property(a => a.IdParent);
            builder.Property(a => a.Sort).IsRequired();
            builder.Property(a => a.IsAllow).IsRequired();
        }
    }
}
