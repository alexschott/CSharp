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
using System.Windows.Media.Imaging;

namespace Lime.Views
{
    /// <summary>
    /// Interaction logic for WelcomeMore.xaml
    /// </summary>
    public partial class WelcomeMore : UserControl
    {
        public WelcomeMore()
        {
            InitializeComponent();
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\welcome.jpg";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }
    }
}
