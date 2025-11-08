using EcommerceApp.Models;

namespace EcommerceApp.Repositories.Interfaces
{
    public interface IAuthRepositories
    {
        Task<Users?> RegisterUserAsync(Users user);
        Task<Users?> LoginUserAsync(Users user);

    }
}
