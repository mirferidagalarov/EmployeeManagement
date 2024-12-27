using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Name)
             .HasMaxLength(50)
             .IsRequired()
             .IsUnicode()
             .HasComment("Employee Name");

            builder.Property(x => x.Surname)
            .HasMaxLength(100)
            .IsRequired()
            .IsUnicode()
            .HasComment("Employee Surname");

            builder.HasOne(x => x.Department)
           .WithMany(x => x.Employees)
           .HasForeignKey(x => x.DepartmenId)
           .IsRequired();

        }
    }
}
