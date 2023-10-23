using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddEmplyeesRestorandDetailsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW EmployeeWithRestaurantDetailsView AS
                SELECT 
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.Position,
                    r.Name AS RestaurantName,
                    r.Address AS RestaurantAddress
                FROM Employees e
                INNER JOIN Restaurants r ON e.RestaurantId = r.RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW EmployeeWithRestaurantDetailsView");
        }
    }
}
