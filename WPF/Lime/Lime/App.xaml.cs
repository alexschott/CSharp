using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += App_StartUp;
        }

        void App_StartUp(object sender, StartupEventArgs e)
        {
            Lime.MainWindow.Instance.Show();
        }
    }
}
