using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;

namespace watcher.BLL
{
    
    internal class AddA4DeleteA4Class
    {
        TechProcGridCreatingClass _techProcGridCreatingClass = new TechProcGridCreatingClass();
        internal Grid AddA4DeleteA4(Grid curentGrid)
        {
            #region Кнопки добавления/удаления листов
            //Создание кнопки "Добавить новый лист"
            Button button_AddNewList = new Button() { Height = 50, Width = 152, VerticalAlignment = VerticalAlignment.Bottom, Content = "Добавить Лист", Background = new SolidColorBrush(Colors.Bisque), BorderThickness = new Thickness(0, 5, 0, 5), BorderBrush = new SolidColorBrush(Color.FromArgb(255, 180, 120, 120)) };
            Grid.SetColumn(button_AddNewList, 0);
            button_AddNewList.Click += (s,e) => _techProcGridCreatingClass.AddNewA4(s,e,curentGrid);

            //Создание кнопки "Удалить лист"
            Button button_DeleteSheet = new Button() { Height = 50, Width = 152, VerticalAlignment = VerticalAlignment.Bottom, Content = "Удалить Лист", Background = new SolidColorBrush(Colors.Bisque), BorderThickness = new Thickness(0, 5, 0, 5), BorderBrush = new SolidColorBrush(Color.FromArgb(255, 180, 120, 120)) };
            Grid.SetColumn(button_DeleteSheet, 1);
            //button_DeleteSheet.Click += DeleteSheet;

            //создание Grid для кнопок добавления/удаления листов
            Grid gridForButton = new Grid() { VerticalAlignment = VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Center };
            gridForButton.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            gridForButton.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            gridForButton.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            gridForButton.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            gridForButton.Children.Add(button_AddNewList);
            gridForButton.Children.Add(button_DeleteSheet);
            //Grid.SetRow(gridForButton, A4_2.RowDefinitions.Count);
            //A4_2.Children.Add(gridForButton);
            #endregion
            return gridForButton;
        }
    }

}
