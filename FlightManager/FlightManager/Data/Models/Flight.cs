namespace FlightManager.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Flight
    {
        public Flight()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        public string PilotName { get; set; }

        public string FlightFrom { get; set; }

        public string FlightTo { get; set; }

        public DateTime LiftOff { get; set; }

        public DateTime LandOn { get; set; }

        public string TypeOfPlane { get; set; }

        public string UniqueNumber { get; set; }

        public int CapacityPassengers { get; set; }

        public int CapacityBusinessClass { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
