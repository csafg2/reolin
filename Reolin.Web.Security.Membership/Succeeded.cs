using System;

namespace Reolin.Web.Security.Membership
{

    public class SucceedResult : IdentityResult
    {
        public override bool Succeeded
        {
            get
            {
                return true;
            }
        }
    }
}