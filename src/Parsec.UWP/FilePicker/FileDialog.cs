using Parsec.Core.FilePicker;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace Parsec.UWP.FilePicker
{
    public class FileDialog : IFileDialog
    {
        public async Task<string> Open(IEnumerable<string> extensions)
        {
            var picker = new FileOpenPicker();
            foreach(var e in extensions)
                picker.FileTypeFilter.Add(e);

            var file = await picker.PickSingleFileAsync();
            return file?.Path;
        }
    }
}
