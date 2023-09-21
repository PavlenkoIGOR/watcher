using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace watcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double globalHeight = 793.7d;
        private double a4height = 793.7d;

        public MainWindow()
        {
            InitializeComponent();
            forTitlePage.Source = new Uri("TitlePage.xaml", UriKind.Relative);
        }

		private void Click_func(object sender, MouseButtonEventArgs e)
        {
			switch ((sender as Grid).Name)
			{
//				case ("A4"):
//					A4.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(a4height) });
//					A4.Height += globalHeight;
//					A4.ShowGridLines = true;
//					break;
				case("A4_2"):
					A4_2.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(a4height) });
					A4_2.Height += globalHeight;
					A4_2.ShowGridLines = true;
					break;
			}
        }

        private void Click_func2(object sender, RoutedEventArgs e)
        {
            violetGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(250) });
            violetGrid.Height += 250;
            violetGrid.ShowGridLines = true;
        }
        
        private void AddRow(object sender, RoutedEventArgs e)
        {
        	//MessageBox.Show("asdasdasdasda!!!");
            tableWithTechProc.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(18) });
            tableWithTechProc.Height += 18;
            tableWithTechProc.ShowGridLines = true;
        }
    }
}
