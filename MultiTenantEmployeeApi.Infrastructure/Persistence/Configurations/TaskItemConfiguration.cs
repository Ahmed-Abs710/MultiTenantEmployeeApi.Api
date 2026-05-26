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
    //public class TaskItemConfiguration : BaseEntityConfiguration<TaskItem, Guid>
    //{
        //public override void Configure(EntityTypeBuilder<TaskItem> builder)
        //{
        //    base.Configure(builder);
        //    builder.ToTable("Tasks");

        //    builder.Property(x => x.Name)
        //        .HasMaxLength(200)
        //        .IsRequired();

        //    builder.Property(x => x.Description);

        //    builder.Property(x => x.DueDate)
        //        .IsRequired();
        //}
    //}
}
