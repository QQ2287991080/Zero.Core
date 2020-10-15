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
    public class RoleMenuConfig : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            builder.SetEntityConfig();
            builder.Property(a => a.MenuId).IsRequired();
            builder.Property(a => a.RoleId).IsRequired();
            builder.HasOne(a => a.Role).WithMany(a => a.RoleMenus).HasForeignKey(a => a.RoleId);
            builder.HasOne(a => a.Menu).WithMany(a => a.RoleMenus).HasForeignKey(a => a.MenuId);
        }
    }
}
