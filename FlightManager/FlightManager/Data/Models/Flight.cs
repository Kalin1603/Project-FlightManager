namespace FlightManager.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Flight
    {
        public Flight()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        [Display(Name = "Име на пилот")]
        public string PilotName { get; set; }

        [Display(Name = "Полет от")]
        public string FlightFrom { get; set; }

        [Display(Name = "Полет за")]
        public string FlightTo { get; set; }

        [Display(Name = "Дата на излитане")]
        public DateTime LiftOff { get; set; }

        [Display(Name = "Дата на кацане")]
        public DateTime LandOn { get; set; }

        [Display(Name = "Тип на самолета")]
        public string TypeOfPlane { get; set; }

        [Display(Name = "Уникален номер")]
        public string UniqueNumber { get; set; }

        [Display(Name = "Капацитет на пътниците")]
        public int CapacityPassengers { get; set; }

        [Display(Name = "Капацитет на бизнес класа")]
        public int CapacityBusinessClass { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
