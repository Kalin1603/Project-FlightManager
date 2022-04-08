using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.ViewModels.Reservations
{
    public class EditReservationViewModel
    {
        public int Id { get; set; }

        public int PassangersCount { get; set; }

        public string Email { get; set; }

        public FlightReservationViewModel Flight { get; set; }

        public ICollection<PassangerReservationViewModel> Passangers { get; set; }

        public bool IsConfirmed { get; set; }

        public string UserConfirmedAccount { get; set; }
    }
}
