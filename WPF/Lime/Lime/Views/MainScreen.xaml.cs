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
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : UserControl
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StudentView sv = new StudentView();
            MainWindow.Instance.contentMain.Content = sv;
        }

        private void AvailableServers_Click(object sender, RoutedEventArgs e)
        {
            AvailableServers availableServerScreen = AvailableServers.Instance;
            MainWindow.Instance.contentMain.Content = availableServerScreen;
        }

        private void testConnection_Click(object sender, RoutedEventArgs e)
        {
            TestConnection testConScreen = TestConnection.Instance;
            MainWindow.Instance.contentMain.Content = testConScreen;
        }

        private void welcomeBtn_Click(object sender, RoutedEventArgs e)
        {
            Welcome welcomeScreen = new Welcome();
            MainWindow.Instance.contentMain.Content = welcomeScreen;
        }

        private void configBtn_Click(object sender, RoutedEventArgs e)
        {
            Configuration configScreen = Configuration.Instance;
            MainWindow.Instance.contentMain.Content = configScreen;

        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            Report reportScreen = new Report();
            MainWindow.Instance.contentMain.Content = reportScreen;
        }

        private void licenseImage_loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\File Setting-WF.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void reportImage_loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Graph-03blue.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void availImage_loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Grid dark.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void confImage_loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Settings02-WF.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void testImage_loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Connector-01-WF.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void discoveryImage_loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Search-WF.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void sshImage_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\SSH.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void starImage_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Password-Text-01.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void cleanImage_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Clean.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void listAppImage_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\ListApp-WF.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void lockImage_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Lock.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void serverFindImage_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Server-WF.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }

        private void webFindImage_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string imagePath = System.Environment.CurrentDirectory + "\\images\\Webpage Find.png";
            b.UriSource = new Uri(imagePath);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;
        }
    }
}
