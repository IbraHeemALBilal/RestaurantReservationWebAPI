using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;

namespace RestaurantReservation.Db.Config
{
    public class ReservationConfiguraton : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.ReservationId);
            builder.Property(r => r.ReservationId)
                .ValueGeneratedOnAdd();
            builder.Property(r => r.ReservationDate).IsRequired();
            builder.Property(r => r.PartySize).IsRequired();

            builder.HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId);

            builder.HasOne(r => r.Restaurant)
                .WithMany(re => re.Reservations)
                .HasForeignKey(r => r.RestaurantId);

            builder.HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(r => r.Orders)
                .WithOne(o => o .Reservation)
                .HasForeignKey(o => o.ReservationId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.HasData(LoadReservations());
        }

        /*private static List<Reservation> LoadReservations()
        {
            return new List<Reservation>
            {
                new Reservation { ReservationId = 1, CustomerId = 1, RestaurantId = 1, TableId = 1, ReservationDate = DateTime.Now.AddDays(1), PartySize = 4 },
                new Reservation { ReservationId = 2, CustomerId = 2, RestaurantId = 1, TableId = 2, ReservationDate = DateTime.Now.AddDays(2), PartySize = 6 },
                new Reservation { ReservationId = 3, CustomerId = 3, RestaurantId = 2, TableId = 3, ReservationDate = DateTime.Now.AddDays(3), PartySize = 5 },
                new Reservation { ReservationId = 4, CustomerId = 4, RestaurantId = 2, TableId = 4, ReservationDate = DateTime.Now.AddDays(4), PartySize = 8 },
                new Reservation { ReservationId = 5, CustomerId = 5, RestaurantId = 3, TableId = 5, ReservationDate = DateTime.Now.AddDays(5), PartySize = 6 }
            };
        }*/
    }
}
