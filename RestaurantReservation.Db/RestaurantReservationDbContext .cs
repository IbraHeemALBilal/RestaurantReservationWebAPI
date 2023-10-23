using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Views;


namespace RestaurantReservation.Db
{
    public class RestaurantReservationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CustomerWithPartySize> CustomersWithPartySize { get; set; }

        public DbSet<ReservationView> ReservationViews { get; set; }
        public DbSet<EmployeeWithRestaurantDetails> EmployeesWithRestaurantDetails { get; set; }

        public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options): base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-2628EB6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=RestaurantReservationCore");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantReservationDbContext).Assembly);
            modelBuilder.Entity<ReservationView>().HasNoKey().ToView("ReservationView");
            modelBuilder.Entity<EmployeeWithRestaurantDetails>().HasNoKey().ToView("EmployeeWithRestaurantDetailsView");
            modelBuilder.Entity<CustomerWithPartySize>().HasNoKey();

        }
        [DbFunction("CalculateTotalRevenue", schema: "dbo")]
        public static decimal CalculateTotalRevenue(int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
