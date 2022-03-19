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
using Npgsql;

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        NpgsqlConnection conn;
        NpgsqlCommand cmd;
        DataSet ds;
        NpgsqlDataAdapter based;
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
            if (userLogin() == 1)
            {
                userOnline();
                ConnectDB();
            }
            else if (userLogin() == 2)
            {
                error err = new error();
                err.Show();
            }
            else
            {
                error err = new error();
                err.errmsg.Content = "Incorrect login info.";
                err.Show();
            }
            e.Handled = true;
        }
        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if(userLogin()==1)
                {
                    userOnline();
                    ConnectDB();
                }
                else if(userLogin()==2)
                {
                    error err = new error();
                    err.Show();
                }
                else
                {
                    error err = new error();
                    err.errmsg.Content = "Incorrect login info.";
                    err.Show();
                }
                e.Handled = true;
            }
        }
        public int userLogin()
        {
            int correct = 0;
            Password.Password = Password.Password.Replace("\'", "\'\'");
            try
            {
                connectionInfo connect = new connectionInfo(ServerIP.ip);
                conn = new NpgsqlConnection(connect.connectionString);
                conn.Open();
                ds = new DataSet();
                pwdCheck login = new pwdCheck(Name.Text, Password.Password);
                cmd = new NpgsqlCommand(login.pwdChk, conn);
                based = new NpgsqlDataAdapter(cmd);
                based.Fill(ds, "users");
                IList<userList> co1 = new List<userList>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (Name.Text == dr[0].ToString()) correct = 1;
                    else Name.Text = dr[0].ToString();
                }
                cmd.Dispose();
                conn.Close();
                if (correct == 1) return 1;
                else return 0;
            }
            catch (NpgsqlException ex)
            {
                return 2;
            }
        }
        public void userOnline()
        {
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new NpgsqlConnection(connect.connectionString);
            try
            {
                conn.Open();
                userAdd online = new userAdd(Name.Text);
                cmd = new NpgsqlCommand(online.userLogin, conn);
                based = new NpgsqlDataAdapter();
                based.InsertCommand = new NpgsqlCommand(online.userLogin, conn);
                based.InsertCommand.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
            }
        }
    }
}
