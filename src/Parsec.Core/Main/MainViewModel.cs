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

        public MainViewModel(ILoggingService loggingService, IWindowFactory windowFactory, IFileDialog fileDialog, IParsecService parsecService, IVersionService versionService, WorkspaceViewModel workspace)
        {
            _loggingService = loggingService;
            _windowFactory = windowFactory;
            _fileDialog = fileDialog;
            _parsecService = parsecService;
            _versionService = versionService;
            _workspaceViewModel = workspace;

            ShowAboutCommand = new RelayCommand(ShowAbout);
            OpenSahCommand = new AsyncRelayCommand(OpenSah);
            ExractWorkspaceCommand = new AsyncRelayCommand(ExractWorkspace, CanExractWorkspace);
            CollapseAllCommand = new RelayCommand(CollapseAll);

            _workspaceViewModel.IsExtractingChanged += (extracting) => ((AsyncRelayCommand)ExractWorkspaceCommand).NotifyCanExecuteChanged();

            _loggingService.Info(this, $"Started app. Version: {_versionService.GetCurrentVersion()}. Parsec version: {_versionService.GetParsecVersion()}");
        }

        #endregion

        #region Properties

        private readonly WorkspaceViewModel _workspaceViewModel;
        public WorkspaceViewModel WorkspaceViewModel
        {
            get => _workspaceViewModel;
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

            WorkspaceViewModel.Data = data;
            WorkspaceViewModel.LoadFilesTree();
            ((AsyncRelayCommand)ExractWorkspaceCommand).NotifyCanExecuteChanged();
        }

        public ICommand ExractWorkspaceCommand { get; }
        private async Task ExractWorkspace()
        {
            var path = await _fileDialog.OpenFolder();
            if (string.IsNullOrEmpty(path))
                return; // Nothing was selected.

            await WorkspaceViewModel.ExtractCommand.ExecuteAsync(path);
        }

        private bool CanExractWorkspace()
        {
            return WorkspaceViewModel.IsReady && !WorkspaceViewModel.IsExtracting;
        }

        public ICommand CollapseAllCommand { get; }
        private void CollapseAll()
        {
            WorkspaceViewModel.CollapseAll();
        }

        #endregion
    }
}
