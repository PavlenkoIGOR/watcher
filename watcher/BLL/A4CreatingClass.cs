using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace watcher.BLL
{
    internal class A4CreatingClass
    {               
        public A4CreatingClass() 
        {

        }
        internal Grid CreatA4()
        {
            Grid A4 = new Grid() { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Center, Height = 793.7, Width = 1122.5, Background = Brushes.Bisque, Name = "A4" };
            A4.VerticalAlignment = VerticalAlignment.Top;
            A4.HorizontalAlignment = HorizontalAlignment.Center;
            A4.Height = 793.7; 
            A4.Width = 1122.5;
            A4.Background = Brushes.Bisque;
            A4.RowDefinitions.Add(new RowDefinition());
            A4.ColumnDefinitions.Add(new ColumnDefinition());

            return A4;
        }
    }
}
