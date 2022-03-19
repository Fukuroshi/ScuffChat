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
using System.Data;
using System.Data.SqlClient;
using Npgsql;


namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        NpgsqlConnection conn;
        NpgsqlCommand cmd;
        DataSet ds;
        NpgsqlDataAdapter based;

        public void NewAcc()
        {
            Name.Text = Name.Text.Replace("\'", "\'\'");
            Password.Password = Password.Password.Replace("\'", "\'\'");
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new NpgsqlConnection(connect.connectionString);
            try
            {
                conn.Open();
                newAccount acc = new newAccount(Name.Text, Password.Password);
                cmd = new NpgsqlCommand(acc.newAcc, conn);
                based = new NpgsqlDataAdapter();
                based.InsertCommand = new NpgsqlCommand(acc.newAcc, conn);
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
        public bool isUnavailable()
        {
            bool correct = false;
            Password.Password = Password.Password.Replace("\'", "\'\'");
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new NpgsqlConnection(connect.connectionString);
            try
            {
                conn.Open();
                ds = new DataSet();
                availCheck login = new availCheck(Name.Text);
                cmd = new NpgsqlCommand(login.avChk, conn);
                based = new NpgsqlDataAdapter(cmd);
                based.Fill(ds, "users");
                IList<userList> co1 = new List<userList>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (Name.Text.ToLower() == dr[0].ToString().ToLower()) correct = true;
                }
                cmd.Dispose();
                conn.Close();
                if (correct == true) return true;
                else return false;
            }
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
                return false;
            }
        }

        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {
            if (isUnavailable() == true)
                Name.Text = "Username taken.";
            else
            {
                NewAcc();
                this.Close();
            }
        }
    }
}
