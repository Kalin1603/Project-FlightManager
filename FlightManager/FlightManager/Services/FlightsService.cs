using FlightManager.Data;
using FlightManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly AppDbContext context;

        public FlightsService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Flight>> GetFlightsAsync()
        {
            return await context.Flights.ToListAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await context.Flights
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateFlightASync(Flight flight)
        {
            context.Add(flight);
            await context.SaveChangesAsync();
        }

        public async Task<Flight> FindFlightAsync(int id)
        {
            return await context.Flights.FindAsync(id);
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            context.Update(flight);
            await context.SaveChangesAsync();
        }

        public async Task<Flight> GetIdToDelete(int id)
        {
            return await context.Flights
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteFlightConfirmedAsync(Flight flight)
        {
            context.Flights.Remove(flight);
            await context.SaveChangesAsync();
        }

        public bool CheckFlightExist(int id)
        {
            return context.Flights.Any(e => e.Id == id);
        }
    }
}
