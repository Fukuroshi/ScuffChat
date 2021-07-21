using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
    public class onlineCheck
    {
        public onlineCheck(string name)
        {
            isOnline = "isOnline '" + name + "'";
        }
        public string isOnline { get; set; }
    }
    public class pwdCheck
    {
        public pwdCheck(string name, string pwd)
        {
            pwdChk = "login '" + name + "', '" + pwd + "'";
        }
        public string pwdChk{ get; set; }
    }
    public class availCheck
    {
        public availCheck(string name)
        {
            avChk = "availCheck '" + name + "'";
        }
        public string avChk { get; set; }
    }
    public class messageSend {
        public messageSend(string chosenName, string messageContents)
        {
            sendMessage = "messageSend '" + chosenName + "', N'" + messageContents + "'";
        }
        public string sendMessage { get; set; }
    }
    public class dmSend
    {
        public dmSend(string sender, string recipient, string messageContents)
        {
            sendDM= "sendDM '" + sender + "', '" + recipient + "', N'" + messageContents + "'";
        }
        public string sendDM { get; set; }
    }
    public class newAccount
    {
        public newAccount(string chosenName, string chosenPass)
        {
            newAcc = "register '" + chosenName + "', '" + chosenPass + "'";
        }
        public string newAcc { get; set; }
    }
    public class userAdd
    {
        public userAdd(string chosenName)
        {
            userLogin = "online '" + chosenName + "'";
        }
        public string userLogin { get; set; }
    }
    public class userDel
    {
        public userDel(string chosenName)
        {
            userLogin = "offline '" + chosenName + "'";
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
        public static string dmRecipient { get; set; }
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
    public class DMWindow
    {
        public DMWindow(string sender, string recipient)
        {
            {
                dmString = "dmList " + sender + ", " + recipient;
            }
        }
    public string dmString { get; set; }

    }
    public class DMList
    {
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
        public string MessageContents { get; set; }
    }
}
