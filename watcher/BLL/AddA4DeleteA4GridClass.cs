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
    
    internal class AddA4DeleteA4GridClass
    {
        public TechProcGridCreatingClass _techProcGridCreatingClass = new TechProcGridCreatingClass(); 
		public Creating2x2GridClass _Creating2x2GridClass = new Creating2x2GridClass();
        public SheetAndSheetsGridCreateClass _SheetAndSheetsGridCreateClass = new SheetAndSheetsGridCreateClass();
        public StackCreatingClass _StackCreatingClass = new StackCreatingClass();
		
		public AddA4DeleteA4Class()
		{
			
		}
        public Grid AddA4DeleteA4(Grid curentGrid)
        {
            #region Кнопки добавления/удаления листов
            //Создание кнопки "Добавить новый лист"
            Button button_AddNewList = new Button() { Height = 50, Width = 152, VerticalAlignment = VerticalAlignment.Bottom, Content = "Добавить Лист", Background = new SolidColorBrush(Colors.Bisque), BorderThickness = new Thickness(0, 5, 0, 5), BorderBrush = new SolidColorBrush(Color.FromArgb(255, 180, 120, 120)) };
            Grid.SetColumn(button_AddNewList, 0);
            button_AddNewList.Click += (s,e) => AddNewA4(s,e,curentGrid);

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
            Grid.SetRow(gridForButton, curentGrid.RowDefinitions.Count-1);
            #endregion
            return gridForButton;
        }
        
        /// <summary>
        /// добавляет новый лист А4
        /// </summary>
        public void AddNewA4(object sender, RoutedEventArgs e, Grid currGrid)
        {
            //MessageBox.Show("asdasdasdasda!!!");
            if (sender is Button)
            {
                //MessageBox.Show("сработал if");
                //A4_2.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10) }); // для промежутка между листами А4
                currGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(793.7) });
                currGrid.Height += 793.7;
                currGrid.ShowGridLines = true;
                
                //AddA4DeleteA4Class _AddA4DeleteA4Class = new AddA4DeleteA4Class();
                Grid anotherAddA4DeleteA4Grid = AddA4DeleteA4(currGrid);
                

                
                Grid anotherSheetAndSheetsGrid = _SheetAndSheetsGridCreateClass.CreateSheetAndSheetsGrid();
                Grid.SetRow(anotherSheetAndSheetsGrid, 1);
                Grid.SetColumn(anotherSheetAndSheetsGrid, 1);
                
                Grid anotherGridWithTechProc = _techProcGridCreatingClass.CreateMainTable();
                Grid.SetRow(anotherGridWithTechProc, 0);
                Grid.SetColumn(anotherGridWithTechProc, 1);
                                
                StackPanel anotherStackPanel = _StackCreatingClass.CreateStackPanelIntoTeckProcesstable();
                Grid.SetRow(anotherStackPanel, anotherGridWithTechProc.RowDefinitions.Count-1);
                Grid.SetColumn(anotherStackPanel, 4);
                Grid.SetColumnSpan(anotherStackPanel, 2);
                anotherGridWithTechProc.Children.Add(anotherStackPanel);
                
                Grid another2x2Grid = _Creating2x2GridClass.Creating2x2Grid();
                Grid.SetRow(another2x2Grid, currGrid.RowDefinitions.Count-1);
                another2x2Grid.Children.Add(anotherGridWithTechProc);
                another2x2Grid.Children.Add(anotherSheetAndSheetsGrid);
                
                
                currGrid.Children.Add(another2x2Grid);
                currGrid.Children.Add(anotherAddA4DeleteA4Grid);

                //return;
            }
            else
            {
                return;
            }
        }
	            /// <summary>
        /// Удаляет последний лист А4
        /// </summary>
        public void DeleteSheet(object sender, RoutedEventArgs e, Grid currGrid)
        {
            if (sender is Button)
            {
            	int rowIndex = currGrid.RowDefinitions.Count;            	
            	currGrid.Children.RemoveAt(rowIndex-1);
            	currGrid.RowDefinitions.RemoveAt(rowIndex-1);//удаление по индексу
            	for (int i = 0; i < currGrid.RowDefinitions.Count; i++)
            	{
            		//UIElement element = currGrid.Children[i];            		
            		currGrid.RowDefinitions[i].Height=new GridLength(793.7);           		
            	}            	
            	currGrid.Height = currGrid.RowDefinitions.Count * 793.7; 
           		
            	GC.Collect();
            	GC.WaitForPendingFinalizers();
            }
            else
            {
                return;
            }
            
            
            //из try ChatGpt
//            private void RemoveGridRow(Grid grid, int rowIndex)
//{
//// Удаление всех дочерних элементов Grid в заданной строке
//for (int i = grid.Children.Count - 1; i >= 0; i--)
//{
//var child = grid.Children[i];
//if (Grid.GetRow(child) == rowIndex)
//{
//grid.Children.Remove(child);
//}
//}
//
//// Удаление определения строки
//grid.RowDefinitions.RemoveAt(rowIndex);
//
//// Обновление индексов для оставшихся строк
//for (int i = rowIndex; i < grid.RowDefinitions.Count; i++)
//{
//foreach (var child in grid.Children)
//{
//if (Grid.GetRow(child) == i)
//{
//Grid.SetRow(child, i - 1);
//}
//}
//}
//}
//            int rowIndex = 0; // Индекс удаляемой строки
//
//// Удаление строки Grid
//if (rowIndex < grid.RowDefinitions.Count)
//{
//RemoveGridRow(grid, rowIndex);
//}
    }

}
