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
    public class JobConfig : IEntityTypeConfiguration<Jobs>
    {
        public void Configure(EntityTypeBuilder<Jobs> builder)
        {
            builder.SetEntityConfig();
            builder.Property(a => a.Name).HasMaxLength(200).IsRequired();
            builder.Property(a => a.Remark);
            builder.Property(a => a.LastTime);
            builder.Property(a => a.ExecuteCount).IsRequired();
            builder.Property(a => a.StartTime);
            builder.Property(a => a.EndTime);
            //builder.Property(a => a.JobKey).IsRequired();
            //builder.Property(a => a.JobGroup).IsRequired();
            //builder.Property(a => a.TriggerKey).IsRequired();
            builder.Property(a => a.AssemblyName);
            builder.Property(a => a.ClassName);
            builder.Property(a => a.Status);

            builder.Property(a => a.TriggerType);
            builder.Property(a => a.TriggerInterval);
            builder.Property(a => a.CronExpression);
            builder.Property(a => a.Intervals);
        }
    }
}
