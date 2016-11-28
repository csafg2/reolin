namespace Reolin.Web.Security.Membership.Core
{
    public class Failed : IdentityResult
    {
        public override bool Succeeded
        {
            get
            {
                return false;
            }
        }
    }
}