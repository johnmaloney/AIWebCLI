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
            if (string.IsNullOrWhiteSpace(location))
            {
                return false;
            }

            // Check if the path exists
            if (File.Exists(location))
            {
                // Get the file attributes for file or directory
                FileAttributes attr = File.GetAttributes(location);
                // Check if it's a directory
                return attr.HasFlag(FileAttributes.Directory);
            }
            else if (Directory.Exists(location))
            {
                return true;
            }

            // If it doesn't exist, we can infer if it's meant to be a directory by the presence of an extension
            var hasExtension = Path.GetExtension(location);
            return string.IsNullOrEmpty(hasExtension);
        }
    }
}
