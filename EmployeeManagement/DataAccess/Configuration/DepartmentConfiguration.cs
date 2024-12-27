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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(x => x.Name)
                   .HasMaxLength(50)
                   .IsRequired()
                   .IsUnicode()
                   .HasComment("Department Name");

            builder.HasOne(x=>x.Company)
                   .WithMany(x=>x.Departments)
                   .HasForeignKey(x=>x.CompanyId)
                   .IsRequired();

            builder.HasIndex(x => x.Name)
                   .HasDatabaseName("idx_Department_Name");
        }         
    }
}
