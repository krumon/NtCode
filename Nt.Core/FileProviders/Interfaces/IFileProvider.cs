using Nt.Core.Primitives;

namespace Nt.Core.FileProviders
{
    //
    // Resumen:
    //     A read-only file provider abstraction.
    public interface IFileProvider
    {
        //
        // Resumen:
        //     Locate a file at the given path.
        //
        // Parámetros:
        //   subpath:
        //     Relative path that identifies the file.
        //
        // Devuelve:
        //     The file information. Caller must check Exists property.
        IFileInfo GetFileInfo(string subpath);

        //
        // Resumen:
        //     Enumerate a directory at the given path, if any.
        //
        // Parámetros:
        //   subpath:
        //     Relative path that identifies the directory.
        //
        // Devuelve:
        //     Returns the contents of the directory.
        IDirectoryContents GetDirectoryContents(string subpath);

        //
        // Resumen:
        //     Creates a Microsoft.Extensions.Primitives.IChangeToken for the specified filter.
        //
        // Parámetros:
        //   filter:
        //     Filter string used to determine what files or folders to monitor. Example: **/*.cs,
        //     *.*, subFolder/**/*.cshtml.
        //
        // Devuelve:
        //     An Microsoft.Extensions.Primitives.IChangeToken that is notified when a file
        //     matching filter is added, modified or deleted.
        IChangeToken Watch(string filter);
    }
}
