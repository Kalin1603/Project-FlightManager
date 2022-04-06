using FlightManager.ViewModels.Users;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public interface IUsersService
    {
        Task CreateUserAsync(CreateUserViewModel model);
        Task DeleteUserAsync(string id);
        Task EditUserAsync(EditUserViewModel model);
        Task<IndexUsersViewModel> GetUsersAsync(IndexUsersViewModel model);
        Task<EditUserViewModel> GetUserToEdit(string id);
        Task<DetailsUserViewModel> GetUserDetailsAsync(string id);
    }
}
