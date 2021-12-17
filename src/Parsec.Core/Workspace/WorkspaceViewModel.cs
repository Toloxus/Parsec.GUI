using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Parsec.Core.Parsec;
using Parsec.Shaiya.Data;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Parsec.Core.Workspace
{
    public class WorkspaceViewModel : ObservableObject
    {
        #region Constructor
        private readonly IParsecService _parsecService;

        public WorkspaceViewModel(IParsecService parsecService)
        {
            _parsecService = parsecService;

            ExtractSafCommand = new AsyncRelayCommand(ExtractSaf);
        }

        #endregion

        #region Properties

        private Data _data;
        public Data Data
        {
            get => _data;
            set
            {
                _data = value;
                Path = System.IO.Path.GetDirectoryName(_data.Sah.Path);
            }
        }

        private string _path;
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        private bool _isExtracting;
        public bool IsExtracting
        {
            get => _isExtracting;
            set => SetProperty(ref _isExtracting, value);
        }

        private bool _isReady;
        public bool IsReady
        {
            get => _isReady;
            set => SetProperty(ref _isReady, value);
        }

        public ObservableCollection<FileViewModel> FilesTree { get; } = new ObservableCollection<FileViewModel>();

        #endregion

        #region Commands

        public AsyncRelayCommand ExtractSafCommand { get; }
        private async Task ExtractSaf()
        {
            IsExtracting = true;

            var extracted = await _parsecService.TryExtractSaf(Path, _data);
            IsExtracting = false;

            if (!extracted)
                return;

            Load();
        }

        /// <summary>
        /// Loads files from workspace.
        /// </summary>
        public void Load()
        {
            var root = new FileViewModel(Path);
            LoadFolder(root);

            foreach (var child in root.Children)
                FilesTree.Add(child);

            IsReady = true;
        }

        /// <summary>
        /// Recursive load folders and files.
        /// </summary>
        private void LoadFolder(FileViewModel vm)
        {
            var folders = Directory.GetDirectories(vm.Path).OrderBy(x => x);
            foreach (var path in folders)
            {
                var folder = new FileViewModel(path);
                vm.Children.Add(folder);
                LoadFolder(folder);
            }

            var files = Directory.GetFiles(vm.Path).OrderBy(x => x);
            foreach (var path in files)
                vm.Children.Add(new FileViewModel(path));
        }

        #endregion
    }
}
