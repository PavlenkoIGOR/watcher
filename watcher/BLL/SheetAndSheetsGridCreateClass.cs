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
    internal class SheetAndSheetsGridCreateClass
    {
        /// <summary>
        /// Метод для создания таблицы "Лист/Листов"
        /// </summary>
        /// <returns>Grid</returns>
        internal Grid CreateSheetAndSheetsGrid()
        {
            TextBox tBoxNumber = new TextBox() { FontSize = 10, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)) };
            Grid.SetRow(tBoxNumber, 0);
            Grid.SetColumn(tBoxNumber, 0);
            Grid.SetColumnSpan(tBoxNumber, 2);

            TextBox tSheet = new TextBox() { FontSize = 10, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), Text = "Лист", VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            Grid.SetRow(tSheet, 1);
            Grid.SetColumn(tSheet, 0);

            TextBox tSheets = new TextBox() { FontSize = 10, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), Text = "Листов", VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            Grid.SetRow(tSheets, 1);
            Grid.SetColumn(tSheets, 1);

            TextBox tSheetNumber = new TextBox() { FontSize = 10, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Background = Brushes.White, Text = "№Листа", VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            Grid.SetRow(tSheetNumber, 2);
            Grid.SetColumn(tSheetNumber, 0);

            TextBox tSheetsNumber = new TextBox() { FontSize = 10, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Background = Brushes.White, Text = "Кол-во", VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            Grid.SetRow(tSheetsNumber, 2);
            Grid.SetColumn(tSheetsNumber, 1);

            Grid sheetsAndSheet = new Grid() { ShowGridLines = false, HorizontalAlignment = HorizontalAlignment.Right };
            sheetsAndSheet.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(18.9) });
            sheetsAndSheet.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(18.9) });
            sheetsAndSheet.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(18.9) });
            sheetsAndSheet.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(56.7) });
            sheetsAndSheet.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(75.6) });
            Grid.SetRow(sheetsAndSheet, 1);
            Grid.SetColumn(sheetsAndSheet, 1);
            sheetsAndSheet.Children.Add(tBoxNumber);
            sheetsAndSheet.Children.Add(tSheet);
            sheetsAndSheet.Children.Add(tSheets);
            sheetsAndSheet.Children.Add(tSheetNumber);
            sheetsAndSheet.Children.Add(tSheetsNumber);

            return sheetsAndSheet;
        }
    }
}
