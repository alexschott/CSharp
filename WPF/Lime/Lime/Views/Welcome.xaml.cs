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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lime.Views
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            WelcomeMore welcomeMoreScreen = new WelcomeMore();
            MainWindow.Instance.contentMain.Content = welcomeMoreScreen;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            MainWindow.Instance.contentMain.Content = mainScreen;
        }
    }
}
