using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        private OrderRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task CreateAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            var employeeWithOrders = await _dbContext.Employees
                .Where(e => e.EmployeeId == employeeId)
                .Include(e => e.Orders)
                .FirstOrDefaultAsync();

            if (employeeWithOrders != null)
            {
                var averageTotalAmount = employeeWithOrders.Orders.Average(order => order.TotalAmount);
                return averageTotalAmount;
            }

            return 0; 
        }
    }
}
