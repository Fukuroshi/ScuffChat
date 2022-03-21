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
using Npgsql;

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for DM.xaml
    /// </summary>
    public partial class DM : Window
    {
        static connectionInfo connect = new connectionInfo(ServerIP.ip);
        NpgsqlConnection conn = new NpgsqlConnection(connect.connectionString);
        NpgsqlCommand cmd;
        DataSet dms;
        NpgsqlDataAdapter based;
        string recipient = userData.dmRecipient;
        string title;

        public DM()
        {
            InitializeComponent();
            try
            {
                conn.Open();
            }
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
            }
            DMCount.currentCount = 0;
            title = recipient + " - " + onlineChecker();
            this.Title = title;
            GetMSGCount();
            UsernameLabel.Content = userData.username.Replace("\'\'", "\'") + ":";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            title = recipient + " - " + onlineChecker();
            this.Title = title;
            GetMSGCount();
        }

        public void SendMSG()
        {
            MsgBox.Text = MsgBox.Text.Replace("\'", "\'\'");
            try
            {
                dmSend msg = new dmSend(userData.username, recipient, MsgBox.Text);
                cmd = new NpgsqlCommand(msg.sendDM, conn);
                based = new NpgsqlDataAdapter();
                based.InsertCommand = new NpgsqlCommand(msg.sendDM, conn);
                based.InsertCommand.ExecuteNonQuery();
                cmd.Dispose();
                GetMSGCount();
                MsgBox.Text = "";
            }
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
            }
        }

        public void GetMSGCount()
        {
            try
            {
                DMCount dmCounter = new DMCount(userData.username, recipient);
                cmd = new NpgsqlCommand(dmCounter.dmCountString, conn);
                based = new NpgsqlDataAdapter(cmd);
                dms = new DataSet();
                based.Fill(dms, "dms");
                DMCount.newCount = Convert.ToInt32(dms.Tables[0].Rows[0][0].ToString());

                if (this.IsActive == true)
                {
                    if (this.ChatLog.IsMouseOver)
                    {
                        title = title + " - " + (DMCount.newCount - DMCount.currentCount) + " new messages available.";
                    }
                    else if (DMCount.newCount != DMCount.currentCount)
                    {
                        FillMSGList();
                    }
                }
                else
                {
                    title = title + " - " + (DMCount.newCount - DMCount.currentCount) + " new messages available.";
                }
                this.Title = title;
            }
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
            }
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
                DMCount.currentCount = DMCount.newCount;
                DMWindow dmLog = new DMWindow(userData.username, recipient);
                cmd = new NpgsqlCommand(dmLog.dmString, conn);
                based = new NpgsqlDataAdapter(cmd);
                dms = new DataSet();
                based.Fill(dms, "dms");
                IList<DMList> co1 = new List<DMList>();

                foreach (DataRow dr in dms.Tables[0].Rows)
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
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
            }
            finally
            {
                dms = null;
                based.Dispose();
            }
        }
        public string onlineChecker()
        {
            bool online = false;
            string lastOnline = " ";
            try
            {
                dms = new DataSet();
                onlineCheck isOnline = new onlineCheck(recipient);
                cmd = new NpgsqlCommand(isOnline.isOnline, conn);
                based = new NpgsqlDataAdapter(cmd);
                based.Fill(dms, "users");
                IList<userList> co1 = new List<userList>();
                foreach (DataRow dr in dms.Tables[0].Rows)
                {
                    if (dr[0].ToString() == "1") online = true;
                    else
                        lastOnline = dr[1].ToString();
                }
                cmd.Dispose();
                if (online == true) return "Online";
                else return "Last online: " + lastOnline;
            }
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
                return "";
            }
        }
    }
}
