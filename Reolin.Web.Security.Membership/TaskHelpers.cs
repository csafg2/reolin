using System;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership
{

    internal static class TaskHelpers
    {
        public static Task FromException(Exception ex)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            tcs.SetException(ex);
            return tcs.Task;
        }
    }
}