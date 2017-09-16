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
            controlInitialize();
        }

        private void gridInitialize()
        {
            MainGrid.Visibility = Visibility.Visible;
            SettingGrid.Visibility = Visibility.Hidden;
        }

        private void controlInitialize()
        {
            JavaPathComboBox.ItemsSource = SystemTools.FindJava();
            JavaPathComboBox.DisplayMemberPath = "Value";
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

    public class Java
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class Javas : ObservableCollection<Java>
    {
        public Javas()
        {
            
        }
    }
}
