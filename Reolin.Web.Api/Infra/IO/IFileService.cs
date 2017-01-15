using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.IO
{
    /// <summary>
    /// represents file i/o related operations
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// store a stream into the "file name"
        /// </summary>
        /// <param name="input">the input stream</param>
        /// <param name="fileName">the full path to the target file</param>
        /// <returns></returns>
        Task<string> SaveAsync(Stream input, string fileName);
    }
}
