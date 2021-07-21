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
    /// Interaction logic for DM.xaml
    /// </summary>
    public partial class DM : Window
    {

        SqlConnection conn;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter based;
        string recipient = userData.dmRecipient;
        string title;

        public DM()
        {
            InitializeComponent();
            title = recipient + " - " + onlineChecker();
            this.Title = title;
            FillMSGList();
            UsernameLabel.Content = userData.username.Replace("\'\'", "\'") + ":";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            FillMSGList();
            title = recipient + " - " + onlineChecker();
            this.Title = title;
        }

        public void SendMSG()
        {
            MsgBox.Text = MsgBox.Text.Replace("\'", "\'\'");
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new SqlConnection(connect.connectionString);
            conn.Open();
            dmSend msg = new dmSend(userData.username, recipient, MsgBox.Text);
            cmd = new SqlCommand(msg.sendDM, conn);
            based = new SqlDataAdapter();
            based.InsertCommand = new SqlCommand(msg.sendDM, conn);
            based.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            FillMSGList();
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
                DMWindow dmLog = new DMWindow(userData.username, recipient);
                cmd = new SqlCommand(dmLog.dmString, conn);
                SqlDataAdapter BASED = new SqlDataAdapter();
                based = new SqlDataAdapter(cmd);
                ds = new DataSet();
                based.Fill(ds, "dms");
                IList<DMList> co1 = new List<DMList>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    co1.Add(new DMList
                    {
                        Timestamp = Convert.ToDateTime(dr[1].ToString()),
                        UserName = dr[2].ToString(),
                        MessageContents = dr[4].ToString()
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
        public string onlineChecker()
        {
            bool online = false;
            string lastOnline = " ";
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new SqlConnection(connect.connectionString);
            conn.Open();
            ds = new DataSet();
            onlineCheck isOnline = new onlineCheck(recipient);
            cmd = new SqlCommand(isOnline.isOnline, conn);
            based = new SqlDataAdapter(cmd);
            based.Fill(ds, "users");
            IList<userList> co1 = new List<userList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[0].ToString() == "1") online = true;
                else
                    lastOnline = dr[1].ToString();
            }
            cmd.Dispose();
            conn.Close();
            if (online == true) return "Online";
            else return "Last online: " + lastOnline;
        }
    }
}
