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
    public class PhotoManagerConfig : IEntityTypeConfiguration<PhotoManager>
    {

        public void Configure(EntityTypeBuilder<PhotoManager> builder)
        {
            builder.SetEntityConfig();
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200).HasComment("标题");
            builder.Property(a => a.PhotoClass).HasComment("图片样式");
            builder.Property(a => a.Url).IsRequired().HasComment("图片路径");
            builder.Property(a => a.Link).HasComment("图片链接");
        }
    }
}
