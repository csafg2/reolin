
using System;
using System.IO;

namespace Reolin.Web.Api.Infra.IO
{
    public class DirectoryProvider : IDirectoryProvider
    {
        public string ProvideDirectory()
        {
            string guid = Guid.NewGuid().ToString();

            return Path.Combine(guid.Substring(0, 2), guid.Substring(2, 2));
        }
    }

}
