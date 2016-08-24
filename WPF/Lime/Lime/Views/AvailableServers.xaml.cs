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
    /// Interaction logic for AvailableServers.xaml
    /// </summary>
    public partial class AvailableServers : UserControl
    {
        public static AvailableServers Instance { get; private set; }
        static AvailableServers()
        {
            Instance = new AvailableServers();
        }

        private AvailableServers()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            MainWindow.Instance.contentMain.Content = mainScreen;
        }

        private void bottomMenu_Loaded(object sender, RoutedEventArgs e)
        {
            BottomMenu bottomMenu = new BottomMenu();
            bottomMenuContainer.Content = bottomMenu;
        }
    }
}
