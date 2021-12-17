using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Parsec.Core.Version;
using Parsec.Core.FilePicker;
using Parsec.Core.Main;
using Parsec.Core.Parsec;
using Parsec.Core.WindowFactory;
using Parsec.WinUI.About;
using Parsec.WinUI.FilePicker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUiWindowFactory = Parsec.WinUI.WindowFactory;
using Parsec.Core.About;
using Parsec.Core.Logging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Parsec.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            // Build ioc.
            Services = ConfigureServices();

            // Start app.
            _window = Services.GetService<MainWindow>();
            _window.Activate();
        }

        private Window _window;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { private set; get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Setup services.
            services.AddSingleton<ILoggingService, LoggingService>((p) => {
                var logFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Logs\Log.txt");
                return new LoggingService(logFilePath);
            });
            services.AddSingleton<IWindowFactory, WinUiWindowFactory.WindowFactory>();
            services.AddSingleton<IFileDialog, FileDialog>();
            services.AddSingleton<IVersionService, VersionService>();
            services.AddSingleton<IParsecService, ParsecService>();

            // Setup view models.
            services.AddSingleton<AboutViewModel>();
            services.AddSingleton<MainViewModel>();

            // Setup windows.
            services.AddSingleton<MainWindow>();
            services.AddTransient<IAboutWindow, AboutWindow>();

            return services.BuildServiceProvider();
        }
    }
}
