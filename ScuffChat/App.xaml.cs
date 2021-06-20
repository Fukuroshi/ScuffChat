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
    }

    public class messageSend {
        public messageSend(string chosenName, string messageContents)
        {
            sendMessage = "INSERT INTO messages VALUES(GETDATE(), '" + chosenName + "', '" + messageContents + "')";
        }
        public string sendMessage { get; set; }
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
    public class connectionInfo
    {
        public connectionInfo (string serverIPaddr)
        {
            connectionString = @"Data Source ="+serverIPaddr+"; Initial Catalog = ScuffChat; User ID = chat; Password = 12341234";
        }
        public string connectionString { get; set; }
    }
}
