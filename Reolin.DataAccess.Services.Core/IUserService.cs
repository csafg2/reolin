using Reolin.DataAccess.Domain;
using System.Threading.Tasks;

namespace Reolin.DataAccess.Services.Core
{
    public interface IUserService
    {
        Task CreateAsync(User user);
        Task CreateAsync(string userName, byte[] password, string email);
    }
}
