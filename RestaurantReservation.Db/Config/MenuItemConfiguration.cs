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
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(m => m.MenuItemId);
            builder.Property(m => m.MenuItemId)
                .ValueGeneratedOnAdd();
            builder.Property(m => m.Name).IsRequired().HasMaxLength(255);
            builder.Property(m => m.Description).HasMaxLength(1000);
            builder.Property(m => m.Price).HasColumnType("decimal(18, 2)").IsRequired();

            builder.HasOne(m => m.Restaurant)
                .WithMany(r => r.MenuItems)
                .HasForeignKey(m => m.RestaurantId);

            //builder.HasData(LoadMenuItems());
        }

        /*private static List<MenuItem> LoadMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem { MenuItemId = 1, RestaurantId = 1, Name = "Burger", Description = "Delicious beef burger with lettuce, tomato, and cheese", Price = 9.99m },
                new MenuItem { MenuItemId = 2, RestaurantId = 1, Name = "Pizza", Description = "Margherita pizza with fresh mozzarella and basil", Price = 12.99m },
                new MenuItem { MenuItemId = 3, RestaurantId = 2, Name = "Pasta", Description = "Spaghetti with homemade marinara sauce", Price = 10.49m },
                new MenuItem { MenuItemId = 4, RestaurantId = 2, Name = "Salad", Description = "Fresh garden salad with mixed greens and vinaigrette dressing", Price = 7.99m },
                new MenuItem { MenuItemId = 5, RestaurantId = 3, Name = "Sushi", Description = "Assorted sushi rolls with soy sauce and wasabi", Price = 15.99m }
            };
        }*/
    }
}
