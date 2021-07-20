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
using System.Windows.Threading;
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
            FillMSGList();
            FillUserList();
            UsernameLabel.Content = userData.username.Replace("\'\'", "\'") + ":";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            FillMSGList();
            FillUserList();
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
            FillMSGList();
            FillUserList();
            conn.Close();
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

        public void FillMSGList()
        {
            try
            {
                connectionInfo connect = new connectionInfo(ServerIP.ip);
                conn = new SqlConnection(connect.connectionString);
                conn.Open();
                cmd = new SqlCommand("messageList", conn);
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
                ChatLog.SelectedIndex = ChatLog.Items.Count - 1;
                ChatLog.ScrollIntoView(ChatLog.SelectedItem);
                ChatLog.SelectedIndex = -1;
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
        public void FillUserList()
        {
            try
            {
                connectionInfo connect = new connectionInfo(ServerIP.ip);
                conn = new SqlConnection(connect.connectionString);
                conn.Open();
                cmd = new SqlCommand("userList", conn);
                SqlDataAdapter BASED = new SqlDataAdapter();
                based = new SqlDataAdapter(cmd);
                ds = new DataSet();
                based.Fill(ds, "online_users");
                userList msg = new userList();
                IList<userList> co1 = new List<userList>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    co1.Add(new userList
                    {
                        UserName = dr[1].ToString(),
                    });
                }
                UserList.ItemsSource = co1;
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
