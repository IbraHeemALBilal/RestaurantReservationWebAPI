using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;
using System.Collections.Generic;

namespace RestaurantReservation.Db.Config
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(t => t.TableId);
            builder.Property(t => t.TableId)
                .ValueGeneratedOnAdd();
            builder.Property(t => t.Capacity).IsRequired();

            builder.HasOne(t => t.Restaurant)
                .WithMany(r => r.Tables)
                .HasForeignKey(t => t.RestaurantId);

            //builder.HasData(LoadTables());
        }

        /*private static List<Table> LoadTables()
        {
            return new List<Table>
            {
                new Table { TableId = 1, RestaurantId = 1, Capacity = 4 },
                new Table { TableId = 2, RestaurantId = 1, Capacity = 6 },
                new Table { TableId = 3, RestaurantId = 2, Capacity = 5 },
                new Table { TableId = 4, RestaurantId = 2, Capacity = 8 },
                new Table { TableId = 5, RestaurantId = 3, Capacity = 6 }
            };
        }*/
    }
}
