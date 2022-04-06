namespace FlightManager.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        public User()
        {
            EmailConfirmed = true;
        }

        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string NationalId { get; set; }

        public string Address { get; set; }
    }
}
