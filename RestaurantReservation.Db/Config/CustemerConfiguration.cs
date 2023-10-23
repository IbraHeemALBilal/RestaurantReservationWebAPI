using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Config
{
    public class CustemerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.CustomerId)
            .ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.HasIndex(x => x.PhoneNumber).IsUnique();

            builder.HasMany(c => c.Reservations)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId);
            //builder.HasData(LoadCustomers());
        }


        /*private static List<Customer> LoadCustomers()
        {
            return new List<Customer>
            {
            new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", PhoneNumber = "123-456-7890" },
            new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", PhoneNumber = "987-654-3210" },
            new Customer { CustomerId = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", PhoneNumber = "555-555-5555" },
            new Customer { CustomerId = 4, FirstName = "Bob", LastName = "Williams", Email = "bob@example.com", PhoneNumber = "777-777-7777" },
            new Customer { CustomerId = 5, FirstName = "Eva", LastName = "Brown", Email = "eva@example.com", PhoneNumber = "888-888-8888" }
            };
        }*/

    }
}
