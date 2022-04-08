using FlightManager.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public interface IPassangersService
    {
        Task<Passanger> GetPassangerByIdAsync(int id);

        Task<Reservation> GetReservationByIdAsync(int id);

        Task<ICollection<Reservation>> GetReservationsAsync();

        Task<ICollection<Passanger>> GetPassangersAsync();

        Task<Reservation> FindPassangersReservation(int reservationId);

        int PassangersCountAsync(int reservationId);

        Task CreatePassangerASync(Passanger passanger);

        Task<Passanger> FindPassangersByIdAsync(int id);

        Task UpdatePassangerAsync(Passanger passanger);

        Task DeletePassangerConfirmedAsync(Passanger passanger);

        bool CheckPassangerExist(int id);
    }
}