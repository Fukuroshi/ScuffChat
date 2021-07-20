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
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Register regForm = new Register();
            regForm.ShowDialog();
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
            if (userLogin() == true)
            {
                userOnline();
                ConnectDB();
            }

            else
            {
                Name.Text = "Incorrect login info.";
            }
            e.Handled = true;
        }
        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if(userLogin()==true)
                {
                    userOnline();
                    ConnectDB();
                }
                else
                {
                    Name.Text = "Incorrect login info.";
                }
                e.Handled = true;
            }
        }
        public bool userLogin()
        {
            bool correct = false;
            Password.Password = Password.Password.Replace("\'", "\'\'");
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new SqlConnection(connect.connectionString);
            conn.Open();
            ds = new DataSet();
            pwdCheck login = new pwdCheck(Name.Text, Password.Password);
            cmd = new SqlCommand(login.pwdChk, conn);
            based = new SqlDataAdapter(cmd);
            based.Fill(ds, "users");
            IList<userList> co1 = new List<userList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (Name.Text == dr[0].ToString()) correct = true;
            }
            cmd.Dispose();
            conn.Close();
            if (correct == true) return true;
            else return false;
        }
        public void userOnline()
        {
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new SqlConnection(connect.connectionString);
            conn.Open();
            userAdd online = new userAdd(Name.Text);
            cmd = new SqlCommand(online.userLogin, conn);
            based = new SqlDataAdapter();
            based.InsertCommand = new SqlCommand(online.userLogin, conn);
            based.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
    }
}
