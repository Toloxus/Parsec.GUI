using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parsec.Core.FilePicker
{
    public interface IFileDialog
    {
        /// <summary>
        /// Opens file dialog.
        /// </summary>
        /// <param name="ext">allowed file extensions</param>
        /// <returns>path to file</returns>
        Task<string> Open(IEnumerable<string> extensions);
    }
}
