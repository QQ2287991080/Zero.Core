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
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.SetEntityConfig();
            builder.Property(a => a.Name).HasMaxLength(200).IsRequired();
            builder.HasIndex(a => a.Code).IsUnique();//唯一约束
            builder.Property(a => a.Code).HasMaxLength(200).IsRequired();
            builder.Property(a => a.IsAllow).IsRequired();
            builder.Property(a => a.Memo).HasMaxLength(1000);
            builder.HasOne(a => a.Menu).WithMany(a => a.Permissions).HasForeignKey(a => a.MenuId);
        }
    }
}
