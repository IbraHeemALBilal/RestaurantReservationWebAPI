using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        public readonly RestaurantReservationDbContext _dbContext;
        public CustomerRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }
        public async Task<Customer> GetByIdAsync(int customerId)
        {
            return await _dbContext.Customers.FindAsync(customerId);
        }
        public async Task CreateAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Customer updatedCustomer)
        {
            _dbContext.Customers.Update(updatedCustomer);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int customerId)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Customer not found");
            }
        }
    }
}
