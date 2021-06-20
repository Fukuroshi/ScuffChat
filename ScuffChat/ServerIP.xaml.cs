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
        }

        public static string ip;

        private void SetServerIPAddress_Click(object sender, RoutedEventArgs e)
        {
            ip = ServerIPAddress.Text;
            this.Close();
            
        }
    }
}
