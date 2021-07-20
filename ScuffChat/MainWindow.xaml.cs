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
using System.Data;
using System.Data.SqlClient;

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter based;
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
            userLogin();
            ConnectDB();
        }
        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                userLogin();
                ConnectDB();
                e.Handled = true;
            }
        }
        public void userLogin()
        {
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new SqlConnection(connect.connectionString);
            conn.Open();
            userAdd login = new userAdd(Name.Text);
            cmd = new SqlCommand(login.userLogin, conn);
            based = new SqlDataAdapter();
            based.InsertCommand = new SqlCommand(login.userLogin, conn);
            based.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

    }
}
