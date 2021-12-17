using Microsoft.Toolkit.Mvvm.ComponentModel;
using Parsec.Core.Version;

namespace Parsec.Core.About
{
    public class AboutViewModel : ObservableObject
    {
        #region Constructor

        private readonly IVersionService _versionService;

        public AboutViewModel(IVersionService versionService)
        {
            _versionService = versionService;
        }

        #endregion

        #region Properties

        public string CurrentVersion
        {
            get
            {
                return _versionService.GetCurrentVersion();
            }
        }

        public string ParsecVersion
        {
            get
            {
                return _versionService.GetParsecVersion();
            }
        }

        #endregion
    }
}
