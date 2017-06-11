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

namespace FerieApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow.Resources["BackgroundBrush"] = Properties.Settings.Default.Color;
        }

        private void defaultColorPickedEventHandler(object sender, RoutedEventArgs e)
        {
            SolidColorBrush newBrush = SystemColors.WindowBrush;
            Properties.Settings.Default.Color = newBrush;
            Properties.Settings.Default.Save();

            Application.Current.MainWindow.Resources["BackgroundBrush"] = SystemColors.WindowBrush;
        }

        private void redColorPickedEventHandler(object sender, RoutedEventArgs e)
        {
            var newBrush = new SolidColorBrush(Colors.Red);
            Properties.Settings.Default.Color = newBrush;
            Properties.Settings.Default.Save();

            Application.Current.MainWindow.Resources["BackgroundBrush"] = newBrush;
        }

        private void blueColorPickedEventHandler(object sender, RoutedEventArgs e)
        {
            var newBrush = new SolidColorBrush(Colors.Blue);
            Properties.Settings.Default.Color = newBrush;
            Properties.Settings.Default.Save();

            Application.Current.MainWindow.Resources["BackgroundBrush"] = newBrush;
        }

        private void drawingEventHandler(object sender, RoutedEventArgs e)
        {
            var dlg = new DrawPackageList();
            dlg.Title = "Tegn manuelt!";
            dlg.Show();
        }
    }
}
