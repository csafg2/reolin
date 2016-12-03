
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{
    public interface IUserRoleManager
    {
        Task<int> CreateRole(string role);
        Task<int> AddUserToRole(int userId, int roleId);
        Task<int> AddUserToRole(int userId, string roleName);
        Task<int> AddUserToRole(string userName, string roleName);
             
    }
}
