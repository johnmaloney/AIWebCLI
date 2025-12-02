using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCLI.Core
{
    public static class FileSystemExtensions
    {
        /// <summary>
        /// Discovers if a path is for a directory or a single file
        /// </summary>
        /// <param name="location">Full path to the directory or file</param>
        /// <param name="searchExtension">Sometime an extension is not provided, this allows for an extension such as '.json' or '.txt'</param>
        /// <returns></returns>
        public static bool IsDirectoryPath(this string location)
        {
            var hasExtension = Path.GetExtension(location);

            // Dealing with a Directory //
            if (string.IsNullOrEmpty(hasExtension))
            {
                if (File.Exists(location))
                {
                    // get the file attributes for file or directory
                    FileAttributes attr = File.GetAttributes(location);

                    return (attr.HasFlag(FileAttributes.Directory));
                }
                else
                    return true;
            }
            else if (hasExtension != null)
            {
                if (File.Exists(location))
                {
                    return false;
                }
            }
            return false;
        }
    }
}
