using System;
using System.IO;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.IO
{
    public class FileService: IFileService
    {
        private readonly string _basePath;
        private readonly IDirectoryProvider _provider;

        public FileService(string basePath, IDirectoryProvider provider)
        {
            this._provider = provider;
            this._basePath = basePath;
        }

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
                return Path.Combine(subDirectory, fileName);
            }
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
            string directory = Path.Combine(_basePath, subDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }

}
