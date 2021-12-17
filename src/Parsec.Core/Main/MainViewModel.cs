using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Parsec.Core.Version;
using Parsec.Core.FilePicker;
using Parsec.Core.Logging;
using Parsec.Core.Parsec;
using Parsec.Core.WindowFactory;
using System.Threading.Tasks;
using System.Windows.Input;
using Parsec.Core.About;
using Parsec.Core.Workspace;

namespace Parsec.Core.Main
{
    public class MainViewModel : ObservableObject
    {
        private readonly ILoggingService _loggingService;
        private readonly IWindowFactory _windowFactory;
        private readonly IFileDialog _fileDialog;
        private readonly IParsecService _parsecService;
        private readonly IVersionService _versionService;

        #region Constructor

        public MainViewModel(ILoggingService loggingService, IWindowFactory windowFactory, IFileDialog fileDialog, IParsecService parsecService, IVersionService versionService)
        {
            _loggingService = loggingService;
            _windowFactory = windowFactory;
            _fileDialog = fileDialog;
            _parsecService = parsecService;
            _versionService = versionService;

            ShowAboutCommand = new RelayCommand(ShowAbout);
            OpenSahCommand = new AsyncRelayCommand(OpenSah);
            OpenWorkspaceCommand = new AsyncRelayCommand(OpenWorkspace);

            _loggingService.Info(this, $"Started app. Version: {_versionService.GetCurrentVersion()}. Parsec version: {_versionService.GetParsecVersion()}");
        }

        #endregion

        #region Properties

        private WorkspaceViewModel _workspaceViewModel;
        public WorkspaceViewModel WorkspaceViewModel
        {
            get => _workspaceViewModel;
            set => SetProperty(ref _workspaceViewModel, value);
        }

        #endregion

        #region Commands

        public ICommand ShowAboutCommand { get; }
        private void ShowAbout()
        {
            _windowFactory.CreateWindow<IAboutWindow>();
        }

        public ICommand OpenSahCommand { get; }
        private async Task OpenSah()
        {
            var path = await _fileDialog.OpenFile(new string[2] { FileConstants.SAH_EXTENTION, FileConstants.SAH_EXTENTION.ToUpper() });
            if (string.IsNullOrEmpty(path))
                return; // Nothing was selected.

            _loggingService.Debug(this, $"Opening sah file from path {path}");

            var (parced, data) = await _parsecService.TryOpenSah(path);
            if (!parced)
                return;

            WorkspaceViewModel = new WorkspaceViewModel(_parsecService);
            WorkspaceViewModel.Data = data;
        }

        public ICommand OpenWorkspaceCommand { get; }
        private async Task OpenWorkspace()
        {
            var path = await _fileDialog.OpenFolder();
            if (string.IsNullOrEmpty(path))
                return; // Nothing was selected.

            WorkspaceViewModel = new WorkspaceViewModel(_parsecService);
            WorkspaceViewModel.Path = path;
            WorkspaceViewModel.Load();
        }

        #endregion
    }
}
