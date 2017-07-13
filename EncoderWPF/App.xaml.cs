using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EncoderWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            App.Current.Properties["Parameters"] = e;
        }
    }
}
