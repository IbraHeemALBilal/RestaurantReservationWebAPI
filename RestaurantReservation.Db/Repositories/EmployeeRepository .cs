using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Views;

namespace RestaurantReservation.Db.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public readonly RestaurantReservationDbContext _dbContext;
        public EmployeeRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }
        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }
        public async Task CreateAsync(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Employee not found");
            }
        }
        public async Task<List<Employee>> ListManagersAsync()
        {
            return await _dbContext.Employees.Where(e => e.Position == "Manager").ToListAsync();
        }
        public async Task<List<EmployeeWithRestaurantDetails>> GetEmployeesWithRestaurantDetailsAsync()
        {
            return await _dbContext.EmployeesWithRestaurantDetails.ToListAsync();
        }
        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            var totalAmounts = await _dbContext.Orders
                .Where(order => order.EmployeeId == employeeId)
                .Select(order => order.TotalAmount)
                .ToListAsync(); 

            if (totalAmounts.Count == 0)
            {
                return 0;
            }

            decimal averageOrderAmount = totalAmounts.Average();
            return averageOrderAmount;
        }
    }
}
