using System.Threading.Tasks;

namespace System
{
    public static class TaskExtensions
    {
        public static void Forget(this Task source)
        {
            try
            {
                source.ConfigureAwait(false);
            }
            catch(Exception)
            {
                // ignore this fuck..
            }
        }
    }
}
