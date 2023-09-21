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

namespace watcher
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void Click_func(object sender, MouseButtonEventArgs e)
        {
			switch ((sender as Grid).Name)
			{
				case ("A4"):
					A4.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(793.7) });
					A4.Height += 793.7;
					A4.ShowGridLines = true;
					break;
//				case("A4_2"):
//					A4_2.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(a4height) });
//					A4_2.Height += globalHeight;
//					A4_2.ShowGridLines = true;
//					break;
			}
        }
    }
}
