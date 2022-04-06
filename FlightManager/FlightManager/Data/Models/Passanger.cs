namespace FlightManager.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Passanger
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string NationalId { get; set; }

        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }

        [Required]
        public string TypeOfTicket { get; set; }

        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
