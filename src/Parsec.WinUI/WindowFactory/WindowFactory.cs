using Microsoft.UI.Xaml;
using Parsec.Core.WindowFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsec.WinUI.WindowFactory
{
    public class WindowFactory : IWindowFactory
    {
        private readonly App _currentApp;

        public WindowFactory()
        {
            _currentApp = App.Current as App;
        }

        public void CreateWindow<T>() where T : IWindow
        {
            var window = _currentApp.Services.GetService(typeof(T));
            if (window is null)
                throw new Exception($"There is no window of type: {typeof(T)}. Please check App.xaml.cs if such window is registered!");

            (window as Window).Activate();
        }
    }
}
