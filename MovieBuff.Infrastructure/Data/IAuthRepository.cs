using MovieBuff.Entities;
using MovieBuff.Models;
using System.Threading.Tasks;

namespace MovieBuff.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}