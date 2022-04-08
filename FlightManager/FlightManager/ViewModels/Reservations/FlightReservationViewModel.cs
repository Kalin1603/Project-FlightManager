using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.ViewModels.Reservations
{
    public class FlightReservationViewModel
    {
        public int Id { get; set; }

        public string PilotName { get; set; }

        public string FlightFrom { get; set; }

        public string FlightTo { get; set; }

        public DateTime LiftOff { get; set; }

        public DateTime LandOn { get; set; }

        public string TypeOfPlane { get; set; }

        public string UniqueNumber { get; set; }
    }
}
