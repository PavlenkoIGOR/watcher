using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
using System.Xml;

namespace watcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double globalHeight = 793.7d;
        private double a4height = 793.7d;
        TitlePage titlePage;

        public MainWindow()
        {
            titlePage = new TitlePage();
            InitializeComponent();
            //forTitlePage.Source = new Uri("TitlePage.xaml", UriKind.Relative); //эта строка подключает свой Page. В данный момент не нужна т.к. подключается сейчас через InputPage().
            InputPage();
            
        }

        private void InputPage()
        {
            //MainWindow mainWindow = new MainWindow();
            Frame mainFrame = this.FindName("forTitlePage") as Frame; // Найдите элемент Frame в главном окне
            mainFrame.Navigate(titlePage); // Загружаете вашу страницу во Frame
            this.Show(); // Отображаете главное окно
        }


        //перебор элементов
		private void RecursivelyProcessVisualTree(DependencyObject element)
		{
			// Проверка, является ли элемент контейнером
			if (element is Visual)
			{
				// Обработка текущего элемента
				
				// Рекурсивный обход дочерних элементов
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(element, i);
					RecursivelyProcessVisualTree(child);
					if(element is StackPanel)
					{
						int stackPanelCount = 0;
						stackPanelCount++;
						operationCell.Text = "элемент "+ element.DependencyObjectType.Name +" найден!" + stackPanelCount + " шт " + child.GetType();
					}

					
				}
			}
		}




        private void Renew(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> tools = new Dictionary<string, string>();
            string textBox1Value = String.Empty;
            string textBox2Value = String.Empty;
            int Count = 1;

            
            //titlePage.mainToolsList.Text = toolsCell.Text+", шт. - " + quantityCell.Text;

            //лучше этот перебор сделать рекурсией
            foreach (UIElement element in tableWithTechProc.Children) //перечисление всех дочерних элементов у tableWithTechProc таблицы
            {
                if (element is StackPanel)
                {
                    foreach (UIElement grid in ((StackPanel)element).Children)
                    {                    	
                    	for (int rowIndex = 0; rowIndex < ((Grid)grid).RowDefinitions.Count; rowIndex++)
                    	{
                    		TextBox textBoxColumn1 = ((Grid)grid).Children.Cast<TextBox>().FirstOrDefault(c => Grid.GetRow(c) == rowIndex && Grid.GetColumn(c) == 0);
                    		TextBox textBoxColumn2 = ((Grid)grid).Children.Cast<TextBox>().FirstOrDefault(c => Grid.GetRow(c) == rowIndex && Grid.GetColumn(c) == 1);
                    		if (textBoxColumn1 != null && textBoxColumn2 != null)
                    		{
                    			tools[textBoxColumn1.Text] = textBoxColumn2.Text;
                    		}
                    	}
                    	//tools[((TextBox)(grid as Grid).Children[0]).Text] = ((TextBox)(grid as Grid).Children[1]).Text;
//                        tools.Add(
//                            ((TextBox)(grid as Grid).Children[0]).Text,
//                            ((TextBox)(grid as Grid).Children[1]).Text
//                            );
                    	
                    }
                }
            }
            titlePage.mainToolsList.Clear();
            operationCell.Text = Count.ToString();
            foreach (var item in tools)
            {            	
                titlePage.mainToolsList.Text += item.Key + ", шт. - " + item.Value + Environment.NewLine;
                //titlePage.mainToolsList.Text = String.Join(item + ", шт. - \n", tools.Values);
            }
        }

        /// <summary>
        /// отслеживание текста в textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>





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
        
        /// <summary>
        /// добавляет новую строку в таблицу с тех.процессом
        /// </summary>
        private void AddRow(object sender, RoutedEventArgs e)
        {
        	//MessageBox.Show("asdasdasdasda!!!");
            tableWithTechProc.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(18) });
            tableWithTechProc.Height += 18;
            tableWithTechProc.ShowGridLines = true;

            StackPanel newStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Brushes.Red
            };

            #region добавление в newStackPanel новые TextBox'ы
            TextBox textBoxSP1 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Text = "1" };            
            TextBox textBoxSP2 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Text = "2" };
            textBoxSP1.MouseWheel += AddTextBox;
            Grid.SetColumn(textBoxSP1, 0);
            Grid.SetColumn(textBoxSP2, 1);
            #endregion

            #region создание и добавление своей таблицы в newStackPanel
            Grid newGridIntoStack = new Grid() { VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0) };
            newGridIntoStack.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(158.7) });
            newGridIntoStack.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(37.0) });
            newGridIntoStack.Children.Add(textBoxSP1);
            newGridIntoStack.Children.Add(textBoxSP2);

            newStackPanel.Children.Add(newGridIntoStack);
            Grid.SetRow(newStackPanel, tableWithTechProc.RowDefinitions.Count-1);
            Grid.SetColumn(newStackPanel, 4);
            Grid.SetColumnSpan(newStackPanel, 2);
            tableWithTechProc.Children.Add(newStackPanel);
            #endregion
        }
        private void AddTextBox(object sender, MouseWheelEventArgs e)
        {
            //MessageBox.Show("Работает!!!");
            Grid grid = new Grid() { };
            TextBox txt1 = new TextBox() { Name = "toolsCell", FontSize = 10, Height = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0,0,0,0), BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,1) };
            TextBox txt2 = new TextBox() { Name = "quantityCell", FontSize = 10, Height = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0,0,0,0), BorderBrush = Brushes.Black, BorderThickness = new Thickness(1, 1, 1, 1) };
            
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(18.9) });
            grid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(158.7) });
            grid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(37.0) });
            grid.Children.Add(txt1);
            grid.Children.Add(txt2);
            Grid.SetColumn(txt2, 1);
            StackTools.Children.Add(grid); //добавление в существующий  StackPanel следующей Grid
        }
    }
}
