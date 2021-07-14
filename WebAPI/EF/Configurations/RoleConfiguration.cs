using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.EF.Entities;

namespace WebAPI.EF.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles").HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(20).IsUnicode(false);
            builder.Property(x => x.RoleName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200);
        }
    }
}
