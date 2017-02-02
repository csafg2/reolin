using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.IO
{

    /// <summary>
    /// provides file related services, like saving it in file system
    /// </summary>
    public class FileService : IFileService
    {
        private readonly string _basePath;
        private readonly IDirectoryProvider _provider;

        /// <summary>
        /// initialized a new instance of FileService
        /// </summary>
        /// <param name="basePath">the baseDirecotry to store files in</param>
        /// <param name="provider">a provider that provides subDirectory for storing files</param>
        public FileService(string basePath, IDirectoryProvider provider)
        {
            this._provider = provider;
            this._basePath = basePath;
        }

        /// <summary>
        /// Save the file into target path
        /// </summary>
        /// <param name="input">the stream that contains the fucking file</param>
        /// <param name="fileName">the full file name to write the stream into</param>
        /// <returns>the path in which, file has been stored</returns>
        public async Task<string> SaveAsync(Stream input, string fileName)
        {
            string subDirectory = _provider.ProvideDirectory();
            string directory = GetDirectory(subDirectory);
            string fullPath = Path.Combine(directory, fileName);

            if (File.Exists(fullPath))
            {
                fullPath = RenameFile(fullPath);
            }

            using (var stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                await input.CopyToAsync(stream);
            }
            return Path.Combine(this._basePath, subDirectory, fileName);
        }

        private string RenameFile(string fullPath)
        {
            // append a random 4 character to the end of file name
            string fileName = Path.GetFileNameWithoutExtension(fullPath)
                                + Guid.NewGuid()
                                    .ToString()
                                    .Substring(0, 4);

            string extension = Path.GetExtension(fullPath);
            string directory = Path.GetDirectoryName(fullPath);

            return Path.Combine(directory, fileName + extension);
        }

        /// <summary>
        /// combine base directory and subdirectory and also ensure that it`s ready to be used
        /// </summary>
        /// <param name="subDirectory"></param>
        /// <returns></returns>
        private string GetDirectory(string subDirectory)
        {
            //basePath: some thing like  this: e:\files
            string basePath2 = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, _basePath);
            string directory = Path.Combine(basePath2, subDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }

}
