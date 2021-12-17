using Parsec.Core.FilePicker;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace Parsec.WinUI.FilePicker
{
    public class FileDialog : IFileDialog
    {
        public async Task<string> OpenFile(IEnumerable<string> extensions)
        {
            // File picker has still some issues, see:
            // https://github.com/microsoft/microsoft-ui-xaml/issues/2716#issuecomment-727043010
            // https://github.com/microsoft/microsoft-ui-xaml/issues/3295
            var dialog = new FileOpenPicker()
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            foreach(var e in extensions)
                dialog.FileTypeFilter.Add(e);

            var file = await dialog.PickSingleFileAsync();

            return file.Path;
        }
    }
}
