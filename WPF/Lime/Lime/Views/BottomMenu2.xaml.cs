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
    /// Interaction logic for BottomMenu2.xaml
    /// </summary>
    public partial class BottomMenu2 : UserControl
    {
        public BottomMenu2()
        {
            InitializeComponent();
        }
        private void ImportExport_Click(object sender, RoutedEventArgs e)
        {
            ImportExport importExportForm = new ImportExport();
            importExportForm.ShowDialog();
        }

        private void utilBtn_Click(object sender, RoutedEventArgs e)
        {
            BottomMenuMore bottomMenuMore = new BottomMenuMore();
            TestConnection.Instance.bottomMenuContainer.Content = bottomMenuMore;
        }

    }
}
