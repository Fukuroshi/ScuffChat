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
            this.Resources["bg1"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.bg1));
            this.Resources["bg2"] = (Color)(new ColorConverter().ConvertFrom(Properties.Settings.Default.bg2));
            this.Resources["bg3"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.bg2));
            this.Resources["acc1"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.acc1));
            this.Resources["acc2"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.acc2));
        }
        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            errmsg.Content = "Error connecting to the server.";
            this.Close();
        }
    }
}
