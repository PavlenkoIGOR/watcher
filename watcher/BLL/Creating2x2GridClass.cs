using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace watcher.BLL
{
    internal class Creating2x2GridClass
    {
        internal Grid Creating2x2Grid(/*Grid headGrid*/)
        {
            TextBox textBox = new TextBox() { Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, Text = "6", Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, BorderBrush = Brushes.Black, BorderThickness = new Thickness(2,2,1,1)};
            Grid.SetRow( textBox, 0 );
            Grid.SetColumn( textBox, 0 );

            Grid mainGrid = new Grid()
            {
                Name = "mainGrid",
                Background = new SolidColorBrush(Colors.White),
                Margin = new Thickness(50, 50, 50, 50),
                ShowGridLines = false,
                VerticalAlignment = VerticalAlignment.Top
            };

            //mainGrid.Children.Add(headGrid);

            mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            mainGrid.RowDefinitions.Add(new RowDefinition() /*{ Height = new GridLength(1, GridUnitType.Auto) }*/);
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.Children.Add( textBox );
            return mainGrid;
        }
    }
}
