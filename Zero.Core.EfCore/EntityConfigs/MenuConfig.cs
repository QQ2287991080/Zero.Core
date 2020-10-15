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
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.SetEntityConfig();
            builder.Property(a => a.Name).HasMaxLength(200).IsRequired();
            builder.Property(a => a.Url).HasMaxLength(200);
            builder.Property(a => a.Icon).HasMaxLength(200);
            builder.Property(a => a.IconType);
            builder.Property(a => a.ClassName).HasMaxLength(200);
            builder.Property(a => a.Sort).IsRequired();
            builder.Property(a => a.IsAllow);
            builder.Property(a => a.IdParent);
        }
    }
}
