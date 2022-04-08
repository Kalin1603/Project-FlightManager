using System.ComponentModel.DataAnnotations;

namespace FlightManager.ViewModels.Users
{
    public class IndexUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}
