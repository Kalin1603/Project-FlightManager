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

        [Required]
        public string Email { get; set; }

        public int FlightId { get; set; }

        public virtual Flight Flight { get; set; }

        public int PassangersCount { get; set; }

        public bool IsConfirmed { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Passanger> Passangers { get; set; }
    }
}
