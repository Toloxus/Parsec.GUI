using Microsoft.Extensions.DependencyInjection;
using Parsec.Core.Version;
using Parsec.Core.Main;
using Parsec.Core.WindowFactory;
using WpfFactory = Parsec.WPF.WindowFactory;
using System;
using System.Windows;
using Parsec.Core.FilePicker;
using Parsec.Core.Parsec;
using Parsec.Core.Logging;
using System.IO;
using Parsec.Core.About;
using Parsec.WPF.FilePicker;
using Parsec.WPF.About;
using Parsec.WPF.Main;
using Serilog;

namespace Parsec.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Build ioc.
            Services = ConfigureServices();

            // Start app.
            Current.MainWindow = Services.GetService<MainWindow>();
            Current.MainWindow.Show();

            Current.MainWindow.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Shutdown();
        }

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
            services.AddSingleton<ILoggingService, LoggingService>((p) =>
            {
                var logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Logs\Log.txt");

                // TODO: find better place.
                if (File.Exists(logFilePath))
                    File.Delete(logFilePath);

                var config = LoggingService.GetDefaultConfig(logFilePath);

                // Log to UI.
                var window = p.GetService<MainWindow>();
                config.WriteTo.RichTextBox(window.LogTextBox);

                return new LoggingService(config);
            });

            services.AddSingleton<IWindowFactory, WpfFactory.WindowFactory>();
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
