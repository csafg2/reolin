namespace Reolin.Web.Security.Membership
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