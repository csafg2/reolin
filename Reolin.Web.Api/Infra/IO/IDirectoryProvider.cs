
namespace Reolin.Web.Api.Infra.IO
{

    /// <summary>
    /// represents an object that provides a subdirectory to store the file
    /// </summary>
    public interface IDirectoryProvider
    {
        /// <summary>
        /// Generate subDirectory
        /// </summary>
        /// <returns></returns>
        string ProvideDirectory();
    }
}
