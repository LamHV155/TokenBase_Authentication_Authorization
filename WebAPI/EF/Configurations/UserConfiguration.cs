using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.EF.Entities;

namespace WebAPI.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(20).IsUnicode(false);
            builder.Property(x => x.Password).IsRequired().IsUnicode(false);
            builder.Property(x => x.Email).HasMaxLength(100).IsUnicode(false);
           
        }
    }
}
