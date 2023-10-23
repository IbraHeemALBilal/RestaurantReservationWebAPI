using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Config
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);
            builder.Property(e => e.EmployeeId)
             .ValueGeneratedOnAdd();
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Position).HasMaxLength(100);

            builder.HasOne(e => e.Restaurant)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(e => e.Orders)
                .WithOne(o => o.Employee)
                .HasForeignKey(o => o.EmployeeId);

            //builder.HasData(LoadEmployees());
        }
        /*private static List<Employee> LoadEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                EmployeeId = 1,
                RestaurantId = 1,
                FirstName = "John",
                LastName = "Doe",
                Position = "Server"
                },
                new Employee
                {
                    EmployeeId = 2,
                    RestaurantId = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Position = "Chef"
                },
                new Employee
                {
                    EmployeeId = 3,
                    RestaurantId = 2,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Position = "Server"
                },
                new Employee
                {
                    EmployeeId = 4,
                    RestaurantId = 2,
                    FirstName = "Bob",
                    LastName = "Williams",
                    Position = "Bartender"
                },
                new Employee
                {
                    EmployeeId = 5,
                    RestaurantId = 3,
                    FirstName = "Eva",
                    LastName = "Brown",
                    Position = "Manager"
                },


            };
        }*/
    }
}
