using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.IO
{
    public interface IFileService
    {
        Task<string> SaveAsync(Stream input, string fileName);
    }
}
