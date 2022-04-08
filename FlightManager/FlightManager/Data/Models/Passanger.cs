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
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "ЕГН")]
        public string NationalId { get; set; }

        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Националност")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Тип на билета")]
        public string TypeOfTicket { get; set; }

        [Display(Name = "Резервация")]
        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
