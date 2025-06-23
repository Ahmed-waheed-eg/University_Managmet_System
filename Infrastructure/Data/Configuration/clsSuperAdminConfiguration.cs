using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class clsSuperAdminConfiguration : IEntityTypeConfiguration<SuperAdmin>
    {
        public void Configure(EntityTypeBuilder<SuperAdmin> builder)
        {
            builder.ToTable("SuperAdmins");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(15);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();

        }
    }
}
