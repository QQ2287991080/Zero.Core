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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.SetEntityConfig();
            builder.Property(a => a.UserName).HasMaxLength(200).IsRequired();
            builder.Property(a => a.RealName).HasMaxLength(200).IsRequired();
            builder.Property(a => a.Password).IsRequired();
            builder.Property(a => a.Email);
            builder.Property(a => a.IsLock).IsRequired();
            builder.Property(a => a.Phone);
            builder.Property(a => a.Remark);
            builder.Property(a => a.Salt);
            builder.Property(a => a.Sex);
        }
    }
}
