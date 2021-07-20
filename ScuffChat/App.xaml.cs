using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter based;
        public void userLogoff()
        {
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new SqlConnection(connect.connectionString);
            conn.Open();
            userDel login = new userDel(userData.username);
            cmd = new SqlCommand(login.userLogin, conn);
            based = new SqlDataAdapter();
            based.InsertCommand = new SqlCommand(login.userLogin, conn);
            based.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                userLogoff();
            }
            finally
            {
                base.OnExit(e);
            }
        }
    }

    public class messageSend {
        public messageSend(string chosenName, string messageContents)
        {
            sendMessage = "messageSend '" + chosenName + "', '" + messageContents + "'";
        }
        public string sendMessage { get; set; }
    }
    public class userAdd
    {
        public userAdd(string chosenName)
        {
            userLogin = "userAdd '" + chosenName + "'";
        }
        public string userLogin { get; set; }
    }
    public class userDel
    {
        public userDel(string chosenName)
        {
            userLogin = "userDel '" + chosenName + "'";
        }
        public string userLogin { get; set; }
    }
    public class messageList
    {
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
        public string MessageContents { get; set; }
    }
    public static class userData
    {
        public static string username { get; set; }
    }
    public class userList
    {
        public string UserName { get; set; }
    }
    public class connectionInfo
    {
        public connectionInfo (string serverIPaddr)
        {
            connectionString = @"Data Source ="+serverIPaddr+"; Initial Catalog = ScuffChat; User ID = chat; Password = 12341234";
        }
        public string connectionString { get; set; }
    }
}
