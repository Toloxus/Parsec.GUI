using Microsoft.Win32;
using Parsec.Core.FilePicker;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Parsec.WPF.FilePicker
{
    public class FileDialog : IFileDialog
    {
        public Task<string> OpenFile(IEnumerable<string> extentions)
        {
            var dialog = new OpenFileDialog();

            var sb = new StringBuilder("Shaiya|");
            foreach (var e in extentions)
                sb.Append("*").Append(e).Append(";");

            dialog.Filter = sb.ToString(); //Shaiya|*.sah;*.SAH

            dialog.ShowDialog();

            return Task.FromResult(dialog.FileName);
        }

        public Task<string> OpenFolder()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Title = "Select workspace";
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return Task.FromResult(dialog.FileName);
            }
            else
            {
                return Task.FromResult(string.Empty);
            }
        }
    }
}
