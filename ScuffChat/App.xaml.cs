using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Specialized;
using Npgsql;

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        NpgsqlConnection conn;
        NpgsqlCommand cmd;
        NpgsqlDataAdapter based;
        public void userLogoff()
        {
            try
            {
                connectionInfo connect = new connectionInfo(ServerIP.ip);
                conn = new NpgsqlConnection(connect.connectionString);
                conn.Open();
                userDel login = new userDel(userData.username);
                cmd = new NpgsqlCommand(login.userLogin, conn);
                based = new NpgsqlDataAdapter();
                based.InsertCommand = new NpgsqlCommand(login.userLogin, conn);
                based.InsertCommand.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            catch (NpgsqlException ex)
            {
            }
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
            isOnline = "select * from isOnline('" + name + "')";
        }
        public string isOnline { get; set; }
    }
    public class pwdCheck
    {
        public pwdCheck(string name, string pwd)
        {
            pwdChk = "select * from login('" + name + "', '" + pwd + "')";
        }
        public string pwdChk{ get; set; }
    }
    public class availCheck
    {
        public availCheck(string name)
        {
            avChk = "select * from availCheck('" + name + "')";
        }
        public string avChk { get; set; }
    }
    public class messageSend {
        public messageSend(string chosenName, string messageContents)
        {
            sendMessage = "call messageSend('" + chosenName + "', '" + messageContents + "')";
        }
        public string sendMessage { get; set; }
    }
    public class dmSend
    {
        public dmSend(string sender, string recipient, string messageContents)
        {
            sendDM= "call sendDM('" + sender + "', '" + recipient + "', '" + messageContents + "')";
        }
        public string sendDM { get; set; }
    }
    public class newAccount
    {
        public newAccount(string chosenName, string chosenPass)
        {
            newAcc = "call register('" + chosenName + "', '" + chosenPass + "')";
        }
        public string newAcc { get; set; }
    }
    public class userAdd
    {
        public userAdd(string chosenName)
        {
            userLogin = "call online('" + chosenName + "')";
        }
        public string userLogin { get; set; }
    }
    public class userDel
    {
        public userDel(string chosenName)
        {
            userLogin = "call offline('" + chosenName + "')";
        }
        public string userLogin { get; set; }
    }
    public class messageList
    {
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
        public string MessageContents { get; set; }
    }
    public class messageCount
    {
        public static int currentCount { get; set; }
        public static int newCount { get; set; }
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
            if (serverIPaddr == null) serverIPaddr = Properties.Settings.Default.server_ip;
            connectionString = "Host="+serverIPaddr+";Username=chat;Password=12341234;database=chat";
        }
        public string connectionString { get; set; }
    }
    public class DMWindow
    {
        public DMWindow(string sender, string recipient)
        {
            {
                dmString = "select * from dmList(" + "'" + sender + "', '" + recipient + "')";
            }
        }
        public string dmString { get; set; }
    }
    public class DMCount
    {
        public DMCount(string sender, string recipient)
        {
            {
                dmCountString = "select * from dmAmount('" + sender + "', '" + recipient + "')";
            }
        }
        public string dmCountString { get; set; }
        public static int currentCount { get; set; }
        public static int newCount { get; set; }
    }
    public class DMList
    {
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
        public string MessageContents { get; set; }
    }
}
