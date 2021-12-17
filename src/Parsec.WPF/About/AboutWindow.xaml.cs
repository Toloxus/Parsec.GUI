using Parsec.Core.About;
using Parsec.Core.Version;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Parsec.WPF.About
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window, IAboutWindow
    {
        public AboutWindow(AboutViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
