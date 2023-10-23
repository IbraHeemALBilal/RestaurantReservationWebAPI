using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE VIEW ReservationView AS
        SELECT
            r.ReservationId,
            r.CustomerId,
            c.FirstName AS CustomerFirstName,
            c.LastName AS CustomerLastName,
            c.Email AS CustomerEmail,
            r.RestaurantId,
            res.Name AS RestaurantName,
            res.Address AS RestaurantAddress,
            r.ReservationDate
        FROM Reservations r
        JOIN Customers c ON r.CustomerId = c.CustomerId
        JOIN Restaurants res ON r.RestaurantId = res.RestaurantId
        ");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS ReservationView;");
        }
    }
}
