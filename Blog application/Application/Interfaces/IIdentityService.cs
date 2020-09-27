using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task RegisterAsync(string email, string userName, string password);
        Task<string> LoginAsync(string email, string password);
    }
}