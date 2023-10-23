using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories
{
    public class TableRepository : IRepository<Table>
    {
        private readonly RestaurantReservationDbContext _dbContext;


        private TableRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Table>> GetAllAsync()
        {
            return await _dbContext.Tables.ToListAsync();
        }

        public async Task<Table> GetByIdAsync(int tableId)
        {
            return await _dbContext.Tables.FindAsync(tableId);
        }

        public async Task CreateAsync(Table table)
        {
            _dbContext.Tables.Add(table);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Table table)
        {
            _dbContext.Tables.Update(table);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int tableId)
        {
            var table = await _dbContext.Tables.FindAsync(tableId);
            if (table != null)
            {
                _dbContext.Tables.Remove(table);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
