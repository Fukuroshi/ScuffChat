using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Collections.Specialized;

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for ServerIP.xaml
    /// </summary>
    public partial class ServerIP : Window
    {
        public ServerIP()
        {
            InitializeComponent();
            this.Resources["bg1"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.bg1));
            this.Resources["bg2"] = (Color)(new ColorConverter().ConvertFrom(Properties.Settings.Default.bg2));
            this.Resources["bg3"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.bg2));
            this.Resources["acc1"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.acc1));
            this.Resources["acc2"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.acc2));
            ServerIPAddress.Text = Properties.Settings.Default.server_ip;
        }

        public static string ip;

        private void SetServerIPAddress_Click(object sender, RoutedEventArgs e)
        {
            ip = ServerIPAddress.Text;
            Properties.Settings.Default.server_ip = ServerIPAddress.Text;
            this.Close();
        }
    }
}
