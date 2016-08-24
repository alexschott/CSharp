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
    /// Interaction logic for TestConnection.xaml
    /// </summary>
    public partial class TestConnection : UserControl
    {
        public static TestConnection Instance { get; private set; }
        static TestConnection()
        {
            Instance = new TestConnection();
        }

        private TestConnection()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            MainWindow.Instance.contentMain.Content = mainScreen;
        }

        private void ImportExport_Click(object sender, RoutedEventArgs e)
        {
            ImportExport importExportForm = new ImportExport();
            importExportForm.ShowDialog();
        }

        private void bottomMenu_Loaded(object sender, RoutedEventArgs e)
        {
            BottomMenu2 bottomMenu2 = new BottomMenu2();
            bottomMenuContainer.Content = bottomMenu2; 
        }
    }
}
