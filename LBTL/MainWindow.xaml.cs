using System;
using System.Collections.Generic;
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
using LBTL.Global;
using MahApps.Metro.Controls;

namespace LBTL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            gridInitialize();
        }

        private void gridInitialize()
        {
            MainGrid.Visibility = Visibility.Visible;
            SettingGrid.Visibility = Visibility.Hidden;
            
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
    }
}
