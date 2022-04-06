namespace FlightManager.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Reservation
    {
        public Reservation()
        {
            
        }

        public int Id { get; set; }

        public int FlightId { get; set; }

        public virtual Flight Flight { get; set; }

        public int PassangersCount { get; set; }

        public virtual ICollection<Passanger> Passangers { get; set; }
    }
}
