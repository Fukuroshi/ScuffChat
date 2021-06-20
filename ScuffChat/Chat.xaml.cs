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

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter based;

        public Chat()
        {
            InitializeComponent();
            FillList();
            UsernameLabel.Content = userData.username.Replace("\'\'", "\'") + ":";
            ChatLog.SelectedIndex = ChatLog.Items.Count - 1;
            ChatLog.ScrollIntoView(ChatLog.SelectedItem);
            ChatLog.SelectedIndex = -1;
        }

        public void SendMSG()
        {
            MsgBox.Text = MsgBox.Text.Replace("\'", "\'\'");
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new SqlConnection(connect.connectionString);
            conn.Open();
            messageSend msg = new messageSend(userData.username, MsgBox.Text);
            cmd = new SqlCommand(msg.sendMessage, conn);
            based = new SqlDataAdapter();
            based.InsertCommand = new SqlCommand(msg.sendMessage, conn);
            based.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            FillList();
            conn.Close();
            ChatLog.SelectedIndex = ChatLog.Items.Count - 1;
            ChatLog.ScrollIntoView(ChatLog.SelectedItem);
            ChatLog.SelectedIndex = -1;
            MsgBox.Text = "";
        }


        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            SendMSG();
        }

        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SendMSG();
                e.Handled = true;
            }
        }

        public void FillList()
        {
            try
            {
                connectionInfo connect = new connectionInfo(ServerIP.ip);
                conn = new SqlConnection(connect.connectionString);
                conn.Open();
                cmd = new SqlCommand("select * from messages", conn);
                SqlDataAdapter BASED = new SqlDataAdapter();
                based = new SqlDataAdapter(cmd);
                ds = new DataSet();
                based.Fill(ds, "messages");
                messageList msg = new messageList();
                IList<messageList> co1 = new List<messageList>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    co1.Add(new messageList
                    {
                        Timestamp= Convert.ToDateTime(dr[1].ToString()),
                        UserName= dr[2].ToString(),
                        MessageContents = dr[3].ToString()
                    });
                }
                ChatLog.ItemsSource = co1;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ds = null;
                based.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
