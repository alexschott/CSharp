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
    /// Interaction logic for ButtomMenu.xaml
    /// </summary>
    public partial class BottomMenu : UserControl
    {
        public BottomMenu()
        {
            InitializeComponent();
        }

        private void utilBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableServers.Instance.bottomMenuContainer.Content != null)
            {
                BottomMenuMore bottomMenuMoreScreen = new BottomMenuMore();
                AvailableServers.Instance.bottomMenuContainer.Content = bottomMenuMoreScreen;
            }
            if (Configuration.Instance.bottomMenuContainer.Content != null)
            {
                BottomMenuMore bottomMenuMoreScreen = new BottomMenuMore();
                Configuration.Instance.bottomMenuContainer.Content = bottomMenuMoreScreen;
            }
        }
    }
}
