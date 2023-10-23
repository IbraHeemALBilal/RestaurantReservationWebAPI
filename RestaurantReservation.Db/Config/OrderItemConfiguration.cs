using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;

namespace RestaurantReservation.Db.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.OrderItemId);
            builder.Property(oi => oi.OrderItemId)
             .ValueGeneratedOnAdd();
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(oi => oi.MenuItem)
                .WithMany(mi => mi.OrderItems)
                .HasForeignKey(oi => oi.MenuitemId);

            builder.Property(oi => oi.ItemId).IsRequired();
            builder.Property(oi => oi.Quantity).IsRequired();

            //builder.HasData(LoadOrderItems());
        }

        /*private static List<OrderItem> LoadOrderItems()
        {
            return new List<OrderItem>
            {
                new OrderItem { OrderItemId = 1, OrderId = 1, MenuitemId = 1, ItemId = 101, Quantity = 2 },
                new OrderItem { OrderItemId = 2, OrderId = 2, MenuitemId = 2, ItemId = 102, Quantity = 1 },
                new OrderItem { OrderItemId = 3, OrderId = 3, MenuitemId = 3, ItemId = 103, Quantity = 3 },
                new OrderItem { OrderItemId = 4, OrderId = 4, MenuitemId = 4, ItemId = 104, Quantity = 2 },
                new OrderItem { OrderItemId = 5, OrderId = 5, MenuitemId = 5, ItemId = 105, Quantity = 4 }
            };
        }*/
    }
}

        