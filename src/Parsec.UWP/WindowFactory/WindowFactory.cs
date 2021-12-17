using Parsec.Core.WindowFactory;
using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.WindowManagement.Preview;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Parsec.UWP.WindowFactory
{
    public class WindowFactory : IWindowFactory
    {
        private readonly App _currentApp;

        public WindowFactory()
        {
            _currentApp = Application.Current as App;
        }

        public async void CreateWindow<T>() where T : IWindow
        {
            var appWindow = await AppWindow.TryCreateAsync();
            var frame = new Frame();

            var page = _currentApp.Services.GetService(typeof(T));
            frame.Navigate(page.GetType());

            appWindow.RequestSize(new Size(200, 200));

            ElementCompositionPreview.SetAppWindowContent(appWindow, frame);
            await appWindow.TryShowAsync();
        }
    }
}
