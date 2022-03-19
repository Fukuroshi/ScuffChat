﻿using System;
using System.Threading;
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
using Npgsql;

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        NpgsqlConnection conn;
        NpgsqlCommand cmd;
        DataSet ds;
        NpgsqlDataAdapter based;
        string title = "ScuffChat";

        public Chat()
        {
            InitializeComponent();
            messageCount.currentCount = 0;
            GetMSGCount();
            UsernameLabel.Content = userData.username.Replace("\'\'", "\'") + ":";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            GetMSGCount();
        }
        
        public void SendMSG()
        {
            MsgBox.Text = MsgBox.Text.Replace("\'", "\'\'");
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new NpgsqlConnection(connect.connectionString);
            try
            {
                conn.Open();
                messageSend msg = new messageSend(userData.username, MsgBox.Text);
                cmd = new NpgsqlCommand(msg.sendMessage, conn);
                based = new NpgsqlDataAdapter();
                based.InsertCommand = new NpgsqlCommand(msg.sendMessage, conn);
                based.InsertCommand.ExecuteNonQuery();
                cmd.Dispose();
                GetMSGCount();
                conn.Close();
                MsgBox.Text = "";
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

        public void GetMSGCount()
        {
            connectionInfo connect = new connectionInfo(ServerIP.ip);
            conn = new NpgsqlConnection(connect.connectionString);
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand("select * from messageAmount()", conn);
                based = new NpgsqlDataAdapter(cmd);
                ds = new DataSet();
                based.Fill(ds, "messages");
                messageCount.newCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                if (this.IsActive == true)
                {
                    if (this.ChatLog.IsMouseOver)
                    {
                        title = "ScuffChat - " + (messageCount.newCount - messageCount.currentCount) + " new messages available.";
                    }
                    else if (messageCount.newCount != messageCount.currentCount)
                    {
                        FillMSGList();
                        title = "ScuffChat";
                    }
                    FillUserList();
                    FillOfflineUserList();
                }
                else
                {
                    title = "ScuffChat - " + (messageCount.newCount - messageCount.currentCount) + " new messages available.";
                }
                this.Title = title;
            }
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
            }
        }

        public void FillMSGList()
        {
            try
            {
                messageCount.currentCount = messageCount.newCount;
                connectionInfo connect = new connectionInfo(ServerIP.ip);
                conn = new NpgsqlConnection(connect.connectionString);
                conn.Open();
                cmd = new NpgsqlCommand("select * from messageList()", conn);
                NpgsqlDataAdapter BASED = new NpgsqlDataAdapter();
                based = new NpgsqlDataAdapter(cmd);
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
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
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
                conn = new NpgsqlConnection(connect.connectionString);
                conn.Open();
                cmd = new NpgsqlCommand("select * from onlineUsers()", conn);
                based = new NpgsqlDataAdapter(cmd);
                ds = new DataSet();
                based.Fill(ds, "users");
                IList<userList> co1 = new List<userList>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    co1.Add(new userList
                    {
                        UserName = dr[0].ToString(),
                    });
                }
                UserList.ItemsSource = co1;
            }
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
            }
            finally
            {
                ds = null;
                based.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }
        public void FillOfflineUserList()
        {
            try
            {
                connectionInfo connect = new connectionInfo(ServerIP.ip);
                conn = new NpgsqlConnection(connect.connectionString);
                conn.Open();
                cmd = new NpgsqlCommand("select * from offlineUsers()", conn);
                based = new NpgsqlDataAdapter(cmd);
                ds = new DataSet();
                based.Fill(ds, "users");
                IList<userList> co1 = new List<userList>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    co1.Add(new userList
                    {
                        UserName = dr[0].ToString(),
                    });
                }
                OfflineUserList.ItemsSource = co1;
            }
            catch (NpgsqlException ex)
            {
                error err = new error();
                err.Show();
            }
            finally
            {
                ds = null;
                based.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        private void DMOpen_Click(object sender, RoutedEventArgs e)
        {
            userData.dmRecipient = DMName.Text;
            DM dmWindow = new DM();
            dmWindow.Show();
        }
        void DMEnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                userData.dmRecipient = DMName.Text;
                DM dmWindow = new DM();
                dmWindow.Show();
                e.Handled = true;
            }
        }
    }
}
