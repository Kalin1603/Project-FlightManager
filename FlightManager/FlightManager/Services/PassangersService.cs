using FlightManager.Data;
using FlightManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public class PassangersService : IPassangersService
    {
        private readonly AppDbContext context;

        public PassangersService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Passanger>> GetPassangersAsync()
        {
            return await context.Passangers.Include(p => p.Reservation).ToListAsync();
        }

        public async Task<ICollection<Reservation>> GetReservationsAsync()
        {
            return await context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await context.Reservations.FindAsync(id);
        }

        public async Task<Passanger> GetPassangerByIdAsync(int id)
        {
            return await context.Passangers
                 .Include(p => p.Reservation)
                 .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Reservation> FindPassangersReservation(int reservationId)
        {
            return await context.Reservations.FindAsync(reservationId);
        }

        public int PassangersCountAsync(int reservationId)
        {
           return context.Passangers.Count(x => x.ReservationId == reservationId);
        }

        public async Task CreatePassangerASync(Passanger passanger)
        {
            context.Add(passanger);
            await context.SaveChangesAsync();
        }

        public async Task<Passanger> FindPassangersByIdAsync(int id)
        {
            return await context.Passangers.FindAsync(id);
        }

        public async Task UpdatePassangerAsync(Passanger passanger)
        {
            context.Update(passanger);
            await context.SaveChangesAsync();
        }

        public async Task DeletePassangerConfirmedAsync(Passanger passanger)
        {
            context.Passangers.Remove(passanger);
            await context.SaveChangesAsync();
        }

        public bool CheckPassangerExist(int id)
        {
            return context.Passangers.Any(e => e.Id == id);
        }
    }
}
