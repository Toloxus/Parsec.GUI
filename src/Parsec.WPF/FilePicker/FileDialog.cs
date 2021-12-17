using Microsoft.Win32;
using Parsec.Core.FilePicker;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parsec.WPF.FilePicker
{
    public class FileDialog : IFileDialog
    {
        public Task<string> Open(IEnumerable<string> extentions)
        {
            var dialog = new OpenFileDialog();

            var sb = new StringBuilder("Shaiya|");
            foreach (var e in extentions)
                sb.Append("*").Append(e).Append(";");

            dialog.Filter = sb.ToString(); //Shaiya|*.sah;*.SAH

            dialog.ShowDialog();

            return Task.FromResult(dialog.FileName);
        }
    }
}
