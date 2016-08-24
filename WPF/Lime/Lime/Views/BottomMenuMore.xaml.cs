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
    /// Interaction logic for BottomMenuMore.xaml
    /// </summary>
    public partial class BottomMenuMore : UserControl
    {
        public BottomMenuMore()
        {
            InitializeComponent();
        }

        private void backConfigBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (AvailableServers.Instance.bottomMenuContainer.Content != null)
            {
                BottomMenu bottomMenu = new BottomMenu();
                AvailableServers.Instance.bottomMenuContainer.Content = bottomMenu;
            }
            if (TestConnection.Instance.bottomMenuContainer.Content != null)
            {
                BottomMenu2 bottomMenu = new BottomMenu2();
                TestConnection.Instance.bottomMenuContainer.Content = bottomMenu;
            }

            if (Configuration.Instance.bottomMenuContainer.Content != null)
            {
                BottomMenu bottomMenu = new BottomMenu();
                Configuration.Instance.bottomMenuContainer.Content = bottomMenu;
            }
        }
    }
}
