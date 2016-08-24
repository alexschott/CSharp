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
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Lime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static MainWindow Instance { get; private set; }
        static MainWindow()
        {
            Instance = new MainWindow();
        }

        private MainWindow()
        {
            InitializeComponent();
        }
        private void StudentViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Lime.ViewModel.StudentViewModel studentViewModelObject = new Lime.ViewModel.StudentViewModel();
            //studentViewModelObject.LoadStudents();
            //StudentViewControl.DataContext = studentViewModelObject;
        }

        private void move_window(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void Max_processing(object sender, MouseButtonEventArgs e)
        {

        }

        private void Min_processing(object sender, MouseButtonEventArgs e)
        {

        }

        private void Close_processing(object sender, MouseButtonEventArgs e)
        {

        }

        private void Deactivate_Title_Icons(object sender, MouseEventArgs e)
        {

        }

        private void Activate_Title_Icons(object sender, MouseEventArgs e)
        {

        }

        private void EXIT(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MINIMIZE(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MAX_RESTORE(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
    }
}
