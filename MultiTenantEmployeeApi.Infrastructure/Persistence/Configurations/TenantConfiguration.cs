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
    public class TenantConfiguration : BaseEntityConfiguration<Tenant, Guid>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            base.Configure(builder);
            builder.ToTable("Tenants");


            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasData(
                new Tenant
                {
                    ID = Guid.Parse(
                        "11111111-1111-1111-1111-111111111111"),

                    Name = "Tenant A",

                },

                new Tenant
                {
                    ID = Guid.Parse(
                        "22222222-2222-2222-2222-222222222222"),

                    Name = "Tenant B",

                });
        }
    }
}
