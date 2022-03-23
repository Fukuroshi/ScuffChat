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

namespace ScuffChat
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            this.Resources["bg1"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.bg1));
            this.Resources["bg2"] = (Color)(new ColorConverter().ConvertFrom(Properties.Settings.Default.bg2));
            this.Resources["bg3"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.bg2));
            this.Resources["acc1"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.acc1));
            this.Resources["acc2"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.acc2));
            selectedTheme.SelectedValue = Properties.Settings.Default.theme;
            selectedAccents.SelectedValue = Properties.Settings.Default.accents;
        }
        private void SaveOptions_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTheme.SelectedValue.ToString() == "Dark")
            {
                Properties.Settings.Default.bg1 = "#FF343434";
                Properties.Settings.Default.bg2 = "#FF4D4949";
                Properties.Settings.Default.theme = selectedTheme.SelectedValue.ToString();
            }
            else
            {
                Properties.Settings.Default.bg1 = "#FFEFEFEF";
                Properties.Settings.Default.bg2 = "#FFDDDDDD";
                Properties.Settings.Default.theme = selectedTheme.SelectedValue.ToString();
            }
            if(selectedAccents.SelectedValue.ToString() == "Blue")
            {
                if (selectedTheme.SelectedValue.ToString() == "Dark")
                {
                    Properties.Settings.Default.acc1 = "#FF21D7EA";
                    Properties.Settings.Default.acc2 = "#9921D7EA";
                    Properties.Settings.Default.accents = selectedAccents.SelectedValue.ToString();
                }
                else
                {
                    Properties.Settings.Default.acc1 = "#FF113090";
                    Properties.Settings.Default.acc2 = "#99113090";
                    Properties.Settings.Default.accents = selectedAccents.SelectedValue.ToString();
                }
            }
            if (selectedAccents.SelectedValue.ToString() == "Green")
            {
                if (selectedTheme.SelectedValue.ToString() == "Dark")
                {
                    Properties.Settings.Default.acc1 = "#FF00FF00";
                    Properties.Settings.Default.acc2 = "#9900FF00";
                    Properties.Settings.Default.accents = selectedAccents.SelectedValue.ToString();
                }
                else
                {
                    Properties.Settings.Default.acc1 = "#FF009000";
                    Properties.Settings.Default.acc2 = "#99009000";
                    Properties.Settings.Default.accents = selectedAccents.SelectedValue.ToString();
                }
            }
            if (selectedAccents.SelectedValue.ToString() == "Purple")
            {
                if (selectedTheme.SelectedValue.ToString() == "Dark")
                {
                    Properties.Settings.Default.acc1 = "#FFCC21FB";
                    Properties.Settings.Default.acc2 = "#99CC21FB";
                    Properties.Settings.Default.accents = selectedAccents.SelectedValue.ToString();
                }
                else
                {
                    Properties.Settings.Default.acc1 = "#FF601060";
                    Properties.Settings.Default.acc2 = "#99601060";
                    Properties.Settings.Default.accents = selectedAccents.SelectedValue.ToString();
                }
            }
            if (selectedAccents.SelectedValue.ToString() == "Purple/Yellow")
            {
                if (selectedTheme.SelectedValue.ToString() == "Dark")
                {
                    Properties.Settings.Default.acc1 = "#FFFFD11A";
                    Properties.Settings.Default.acc2 = "#99DD41FD";
                    Properties.Settings.Default.accents = selectedAccents.SelectedValue.ToString();
                }
                else
                {
                    Properties.Settings.Default.acc1 = "#FFcc9900";
                    Properties.Settings.Default.acc2 = "#99660066";
                    Properties.Settings.Default.accents = selectedAccents.SelectedValue.ToString();
                }
            }
            Properties.Settings.Default.Save();
            this.Resources["bg1"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.bg1));
            this.Resources["bg2"] = (Color)(new ColorConverter().ConvertFrom(Properties.Settings.Default.bg2));
            this.Resources["bg3"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.bg2));
            this.Resources["acc1"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.acc1));
            this.Resources["acc2"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(Properties.Settings.Default.acc2));
            error restartapply = new error();
            restartapply.errmsg.Content = "Restart app to apply changes.";
            restartapply.Show();
            this.Close();
        }
    }
}
