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
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.RestaurantId);
            builder.Property(r => r.RestaurantId)
            .ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired().HasMaxLength(255);
            builder.Property(r => r.Address).HasMaxLength(255);
            builder.Property(r => r.PhoneNumber).HasMaxLength(20);
            builder.HasIndex(r => r.PhoneNumber).IsUnique();
            builder.Property(r => r.OpeningHours).HasMaxLength(100);

            builder.HasMany(r => r.Employees)
                .WithOne(e => e.Restaurant)
                .HasForeignKey(e => e.RestaurantId);

            builder.HasMany(r => r.Tables)
                .WithOne(t => t.Restaurant)
                .HasForeignKey(t => t.RestaurantId);

            builder.HasMany(r => r.MenuItems)
                .WithOne(m => m.Restaurant)
                .HasForeignKey(m => m.RestaurantId);

            builder.HasMany(r => r.Reservations)
                .WithOne(re => re.Restaurant)
                .HasForeignKey(re => re.RestaurantId);
            //builder.HasData(LoadRestaurants());
        }
        /*private static List<Restaurant> LoadRestaurants()
        {
            return new List<Restaurant>
            {
                  new Restaurant
                  {
                  RestaurantId = 1,
                  Name = "Restaurant 1",
                  Address = "123 Main St",
                  PhoneNumber = "123-456-7890",
                  OpeningHours = "Mon-Sat: 8 AM - 10 PM"
                  },
                  new Restaurant
                  {
                  RestaurantId = 2,
                  Name = "Restaurant 2",
                  Address = "456 Elm St",
                  PhoneNumber = "987-654-3210",
                  OpeningHours = "Mon-Fri: 9 AM - 9 PM"
                  },
                  new Restaurant
                  {
                  RestaurantId = 3,
                  Name = "Restaurant 3",
                  Address = "789 Oak St",
                  PhoneNumber = "555-555-5555",
                  OpeningHours = "Tue-Sun: 7 AM - 11 PM"
                  },
                  new Restaurant
                  {
                  RestaurantId = 4,
                  Name = "Restaurant 4",
                  Address = "101 Pine St",
                  PhoneNumber = "777-777-7777",
                  OpeningHours = "Wed-Mon: 10 AM - 8 PM"
                  },
                  new Restaurant
                  {
                  RestaurantId = 5,
                  Name = "Restaurant 5",
                  Address = "222 Cedar St",
                  PhoneNumber = "888-888-8888",
                  OpeningHours = "Thu-Sun: 9 AM - 7 PM"
                  }
            };
        }*/

    }
}
