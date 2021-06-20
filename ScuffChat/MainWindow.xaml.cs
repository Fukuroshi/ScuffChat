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

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void IPChange_Click(object sender, RoutedEventArgs e)
        {
            ServerIP ipWindow = new ServerIP();
            ipWindow.ShowDialog();
        }

        public void ConnectDB()
        {
            Name.Text = Name.Text.Replace("\'", "\'\'");
            userData.username = Name.Text;
            Chat chatWindow = new Chat();
            chatWindow.Show();
            this.Close();
        }
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectDB();
        }
        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ConnectDB();
                e.Handled = true;
            }
        }
    }
}
