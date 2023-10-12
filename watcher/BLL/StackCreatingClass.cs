using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace watcher.BLL
{
    internal class StackCreatingClass
    {
        
        /// <summary>
        /// Создание StackPanel c TextBoxa'ами
        /// </summary>
        /// <param name="currentGrid"></param>
        internal StackPanel CreateStackPanelIntoTeckProcesstable()
        {
            StackPanel newStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                IsEnabled = true,
                Name = "stackPanel"
            };

            #region  создание textBox'ов для Grid в stackLayout
            TextBox textBoxSP1 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Text = "", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1, 0, 1, 2), FontSize = 10, HorizontalContentAlignment = HorizontalAlignment.Left };
            TextBox textBoxSP2 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Text = "", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1, 0, 1, 2), FontSize = 10, HorizontalContentAlignment = HorizontalAlignment.Center };
            //textBoxSP1.KeyDown += OnTextBoxKeyDown;
            //или так
            textBoxSP1.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(AddTextBoxIntoStackPanel));
            textBoxSP1.TextWrapping = TextWrapping.Wrap;
            textBoxSP1.AcceptsReturn = false;

            textBoxSP2.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(AddTextBoxIntoStackPanel));
            textBoxSP2.TextWrapping = TextWrapping.Wrap;
            textBoxSP2.AcceptsReturn = false;
            Grid.SetColumn(textBoxSP1, 0);
            Grid.SetColumn(textBoxSP2, 1);
            #endregion

            #region добавление в newStackPanel новой таблицы (newGridIntoStack[textBoxSP1|textBoxSP2])
            Grid newGridIntoStack = new Grid() { VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0) };
            newGridIntoStack.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(158.7) });
            newGridIntoStack.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(37.0) });
            newGridIntoStack.Children.Add(textBoxSP1);
            newGridIntoStack.Children.Add(textBoxSP2);

            newStackPanel.Children.Add(newGridIntoStack);
            #endregion
            return newStackPanel;
        }

        ///<summary>
        ///добавление textbox'ов в StackPanel
        ///</summary>
        private void AddTextBoxIntoStackPanel(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
					Grid grid = new Grid() { };
					TextBox txt1 = new TextBox() { Name = "toolsCell", FontSize = 10, Height = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0, 0, 0, 0), BorderBrush = Brushes.Black, BorderThickness = new Thickness(1, 0, 1, 2), HorizontalContentAlignment = HorizontalAlignment.Left };
					txt1.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(AddTextBoxIntoStackPanel));
					TextBox txt2 = new TextBox() { Name = "quantityCell", FontSize = 10, Height = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0, 0, 0, 0), BorderBrush = Brushes.Black, BorderThickness = new Thickness(1, 0, 1, 2), HorizontalContentAlignment = HorizontalAlignment.Center };
					txt2.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(AddTextBoxIntoStackPanel));
					
					grid.RowDefinitions.Add(new RowDefinition() { /*Height = new GridLength(18.9)*/ });
					grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(158.7) });
					grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(37.0) });
					grid.Children.Add(txt1);
					grid.Children.Add(txt2);
					Grid.SetColumn(txt2, 1);

					Grid parentGrid = (sender as TextBox).Parent as Grid;
					StackPanel stPanel = parentGrid.Parent as StackPanel;
					stPanel.Children.Add(grid);
            }
        }
    }

}
