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
using I4GUI;

namespace AgentAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Agent bond = new Agent("007", "Bond, James", "London", "Killing", "Kill Trump");
        public MainWindow()
        {
            InitializeComponent();
            DataContext = bond;
        }

        private void clickcolour(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            //Brush b2 = (Brush)myGrid.FindResource("myBrush");

            //SetResourceReference(Grid.BackgroundProperty, "myBrush");

            Color color = Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255));
            Brush brush = new SolidColorBrush(color);
            this.Resources["myBrush"] = brush;
        }
    }
}
