using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Parsec.Core.Parsec;
using Parsec.Shaiya.Data;
using System;
using System.Collections.ObjectModel;
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

            ExtractCommand = new AsyncRelayCommand<string>(Extract);
        }

        #endregion

        #region Properties

        private Data _data;
        public Data Data
        {
            get => _data;
            set => _data = value;
        }

        private bool _isExtracting;
        public bool IsExtracting
        {
            get => _isExtracting;
            set
            {
                SetProperty(ref _isExtracting, value);
                IsExtractingChanged?.Invoke(_isExtracting);
            }
        }
        public event Action<bool> IsExtractingChanged;

        private bool _isOpeningSah;
        public bool IsOpeningSah
        {
            get => _isOpeningSah;
            set => SetProperty(ref _isOpeningSah, value);
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

        public AsyncRelayCommand<string> ExtractCommand { get; }
        private async Task Extract(string path)
        {
            IsExtracting = true;

            await _parsecService.TryExtractSaf(path, _data);

            IsExtracting = false;
        }

        /// <summary>
        /// Loads files from workspace.
        /// </summary>
        public void LoadFilesTree()
        {
            IsOpeningSah = true;

            var rootVM = new FileViewModel(_data.Sah.RootFolder);
            foreach (var fileVM in rootVM.Children)
                FilesTree.Add(fileVM);

            IsOpeningSah = false;
            IsReady = FilesTree.Count > 0;
        }

        public void CollapseAll()
        {
            foreach (var f in FilesTree)
                f.Collapse();
        }

        #endregion
    }
}
