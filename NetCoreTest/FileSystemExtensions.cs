using System.Collections.Generic;
using System.IO;

namespace NetCoreTest
{
    /// <summary>
    /// Extensions for file system operations.
    /// </summary>
    public static class FileSystemExtensions
    {
        /// <summary>
        /// Recursively creates a directory tree.
        /// </summary>
        /// <param name="di">the deepest directory node in the path</param>
        /// <param name="created">output list of directories created</param>
        public static void CreateDirectory(this DirectoryInfo di, out List<DirectoryInfo> created)
        {
            if (di.Parent != null)
            {
                di.Parent.CreateDirectory(out created);
            }
            else
            {
                created = new List<DirectoryInfo>();
            }

            if (!di.Exists)
            {
                Directory.CreateDirectory(di.FullName);
                created.Add(di);
            }
        }
    }
}
