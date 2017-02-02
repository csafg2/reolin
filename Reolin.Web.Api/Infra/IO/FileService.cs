#pragma warning disable CS1591

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Reolin.Web.Api.Infra.OptionsConfig;
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
        private readonly IDirectoryProvider _provider;
        private readonly UploadDirectorySettings _config;
        private readonly string _webRootPath;


        public FileService(IDirectoryProvider provider,
            IHostingEnvironment env,
            IOptions<UploadDirectorySettings> config)
        {
            this._provider = provider;
            this._config = config.Value;
            this._webRootPath = env.WebRootPath;
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

            return Path.Combine(this._config.BasePath, subDirectory, fileName);
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
            string fullBasePath = Path.Combine(this._webRootPath, this._config.BasePath);
            string directory = Path.Combine(fullBasePath, subDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }
}
