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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class error : Window
    {
        public error()
        {
            InitializeComponent();
        }
        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            errmsg.Content = "Error connecting to the server.";
            this.Close();
        }
    }
}
