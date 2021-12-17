using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Parsec.Core.Workspace
{
    public class FileViewModel : ObservableObject
    {
        #region Constructor

        private string _path;

        public FileViewModel(string path)
        {
            _path = path;
        }

        #endregion

        #region Properties

        public string Name { get => System.IO.Path.GetFileName(_path); }

        public string Path { get => _path; }

        public ObservableCollection<FileViewModel> Children { get; private set; } = new ObservableCollection<FileViewModel>();

        #endregion
    }
}
