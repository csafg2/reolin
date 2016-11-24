using System;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership
{
    public class User
    {
        public string UserName { get; set; }
        public byte[] Password  { get; set; }
    }
    
    public class UserManager
    {
        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }

    public class RoleManager
    {

    }



}
