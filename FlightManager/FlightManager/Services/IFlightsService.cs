using FlightManager.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public interface IFlightsService
    {
        Task<ICollection<Flight>> GetFlightsAsync();

        Task<Flight> GetFlightByIdAsync(int id);

        Task CreateFlightASync(Flight flight);

        Task<Flight> FindFlightAsync(int id);

        Task UpdateFlightAsync(Flight flight);

        Task<Flight> GetIdToDelete(int id);

        Task DeleteFlightConfirmedAsync(Flight flight);

        bool CheckFlightExist(int id);
    }
}