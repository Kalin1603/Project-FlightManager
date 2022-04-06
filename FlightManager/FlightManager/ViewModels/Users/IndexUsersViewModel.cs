using FlightManager.ViewModels.Shared;
using System.Collections.Generic;

namespace FlightManager.ViewModels.Users
{
    public class IndexUsersViewModel : PagingViewModel
    {
        public ICollection<IndexUserViewModel> Users { get; set; }
        public UserFilterViewModel Filter { get; set; } = new UserFilterViewModel();
    }
}
