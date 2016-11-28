using System;

namespace Reolin.Web.Security.Membership.Core
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