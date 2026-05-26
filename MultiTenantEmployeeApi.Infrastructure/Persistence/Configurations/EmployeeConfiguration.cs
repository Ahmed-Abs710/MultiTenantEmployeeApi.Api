using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantEmployeeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : BaseEntityConfiguration<Employee, Guid>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            builder.ToTable("Employees");

            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();


            builder.Property(x => x.Department)
                .HasMaxLength(100);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.CustomData)
                .HasColumnType("jsonb");


            builder.HasIndex(x =>
                new { x.TenantId, x.Email })
                .IsUnique();

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.TenantId);

            builder.OwnsOne(x => x.Salary, m =>
            {
                m.Property(x => x.AmountMinor)
                    .HasColumnName("SalaryAmountMinor");

                m.Property(x => x.CurrencyCode)
                    .HasColumnName("SalaryCurrencyCode")
                    .HasMaxLength(3);
            });

        }
    }
}
