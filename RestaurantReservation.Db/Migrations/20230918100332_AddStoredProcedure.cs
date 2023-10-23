using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE PROCEDURE GetCustomersWithPartySizeGreaterThan
        @PartySize INT
        AS
        BEGIN
            SELECT DISTINCT c.CustomerId, c.FirstName, c.Email, r.PartySize
            FROM Customers c
            JOIN Reservations r ON c.CustomerId = r.CustomerId
            WHERE r.PartySize > @PartySize
        END
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE GetCustomersWithPartySizeGreaterThan");
        }

    }
}
