using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KMCCC.Launcher;
using KMCCC.Tools;
using LBTL.Api;
using LBTL.Global;
using MahApps.Metro.Controls;

namespace LBTL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public bool OnlineMode { get; set; }
        public int MaxMemory { get; set; }
        public string JavaPath { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PlayerName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            gridInitialize();
            controlInitialize();
            propertyInitialize();
        }

        private void propertyInitialize()
        {
            OnlineMode = bool.Parse(DataBaseStorage.GetSettingValue("OnlineMode"));
            MaxMemory = int.Parse(DataBaseStorage.GetSettingValue("MaxMemory"));
            JavaPath = DataBaseStorage.GetSettingValue("JavaPath");
            if ((bool)OnlineModeCheckBox.IsChecked)
            {
                switchToOnline();
                Email = DataBaseStorage.GetSettingValue("Email");
                Password = DataBaseStorage.GetSettingValue("Password");
            }
            else
            {
                switchToOffline();
                PlayerName = DataBaseStorage.GetSettingValue("Player");
            }
        }

        private void gridInitialize()
        {
            MainGrid.Visibility = Visibility.Visible;
            SettingGrid.Visibility = Visibility.Hidden;
        }

        private void controlInitialize()
        {
            SettingGrid.DataContext = this;
            PasswordTextBox.Password = Password;
        }

        private void SettingBackImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainGrid.Visibility = Visibility.Visible;
            SettingGrid.Visibility = Visibility.Hidden;
        }

        private void SettingTile_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            SettingGrid.Visibility = Visibility.Visible;
        }

        private void OnlineModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            switchToOnline();
        }

        private void OnlineModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            switchToOffline();
        }

        private void switchToOnline()
        {
            EmailTextBox.IsEnabled = true;
            EmailTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            PasswordTextBox.IsEnabled = true;
            PasswordTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            PlayerNameTextBox.IsEnabled = false;
            PlayerNameTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
        }

        private void switchToOffline()
        {
            EmailTextBox.IsEnabled = false;
            EmailTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            PasswordTextBox.IsEnabled = false;
            PasswordTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 169, 169, 169));
            PlayerNameTextBox.IsEnabled = true;
            PlayerNameTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }
    }
}
