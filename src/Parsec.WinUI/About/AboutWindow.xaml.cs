using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Parsec.Core.About;
using Parsec.Core.Version;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Parsec.WinUI.About
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutWindow : Window, IAboutWindow
    {
        // Will create exception, not yet supported :(
        // See: https://github.com/microsoft/microsoft-ui-xaml/issues/5289
        public AboutWindow(AboutViewModel vm)
        {
            this.InitializeComponent();
            root.DataContext = vm;
        }

        public void Show()
        {
            Activate();
        }
    }
}
