using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Views;

namespace RestaurantReservation.Db.Repositories
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public ReservationRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int reservationId)
        {
            return await _dbContext.Reservations.FindAsync(reservationId);
        }
        public async Task CreateAsync(Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Reservation reservation)
        {
            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int reservationId)
        {
            var reservation = await _dbContext.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                _dbContext.Reservations.Remove(reservation);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Reservation>> GetReservationsByCustomerAsync(int CustomerId)
        {
            return await _dbContext.Reservations.Where(r=>r.CustomerId== CustomerId).ToListAsync();
        }

        public async Task<List<ReservationView>> GetReservationsWithDetailsAsync()
        {
            return await _dbContext.ReservationViews.ToListAsync();
        }
        public async Task<Reservation> GetReservationWithOrdersAsync(int reservationId)
        {
            return await _dbContext.Reservations
                .Include(r => r.Orders) 
                .ThenInclude(o => o.OrderItems) 
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);
        }
        public async Task<List<MenuItem>> GetOrderedMenuItemsForReservationAsync(int reservationId)
        {
            return await _dbContext.Orders
                .Where(order => order.ReservationId == reservationId)
                .SelectMany(order => order.OrderItems)
                .Select(orderItem => orderItem.MenuItem)
                .ToListAsync();
        }

    }
}
