﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parsec.Core.FilePicker
{
    public interface IFileDialog
    {
        /// <summary>
        /// Opens file selection dialog.
        /// </summary>
        /// <param name="ext">allowed file extensions</param>
        /// <returns>path to file</returns>
        Task<string> OpenFile(IEnumerable<string> extensions);

        /// <summary>
        /// Opens folder selection dialog
        /// </summary>
        /// <returns>path to folder</returns>
        Task<string> OpenFolder();
    }
}
