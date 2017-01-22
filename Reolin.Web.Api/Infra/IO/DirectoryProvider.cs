﻿
using System;
using System.IO;

namespace Reolin.Web.Api.Infra.IO
{
    /// <summary>
    /// Provides a SubDirectory in form of "[A-z][Az]/[A-z][A-z]"
    /// </summary>
    public class TwoCharDirectoryProvider : IDirectoryProvider
    {
        /// <summary>
        /// Generates the SubDirectory
        /// </summary>
        /// <returns></returns>
        public string ProvideDirectory()
        {
            string guid = Guid.NewGuid().ToString();

            return Path.Combine(guid.Substring(0, 2), guid.Substring(2, 2));
        }
    }

}
