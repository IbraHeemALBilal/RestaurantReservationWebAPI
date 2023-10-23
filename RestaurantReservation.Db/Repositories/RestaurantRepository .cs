using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;


namespace RestaurantReservation.Db.Repositories
{
    public class RestaurantRepository : IRepository<Restaurant>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        private RestaurantRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _dbContext.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetByIdAsync(int restaurantId)
        {
            return await _dbContext.Restaurants.FindAsync(restaurantId);
        }

        public async Task CreateAsync(Restaurant restaurant)
        {
            _dbContext.Restaurants.Add(restaurant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _dbContext.Restaurants.Update(restaurant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int restaurantId)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                _dbContext.Restaurants.Remove(restaurant);
                await _dbContext.SaveChangesAsync();
            }
        }
       /* public decimal GetTotalRevenueForRestaurant(int restaurantId)
        {
            var sql = $"SELECT dbo.CalculateTotalRevenue({restaurantId}) AS TotalRevenue";
            var totalRevenue = _dbContext.Database.ExecuteSqlRaw<decimal>(sql);

            return totalRevenue;
        }*/


    }
}
