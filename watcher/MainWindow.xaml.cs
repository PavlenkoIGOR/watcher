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
		/// <summary>
		/// метод для подключения своего Frame
		/// </summary>
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
            int Count = 0;

            //лучше этот перебор сделать рекурсией
            foreach (UIElement element in tableWithTechProc.Children) //перечисление всех дочерних элементов у tableWithTechProc таблицы
            {
                if (element is StackPanel)
                {
                	Count++;
                    foreach (UIElement grid in ((StackPanel)element).Children)
                    { 
                    	if (grid is Grid)
                    	{                    	
                    		for (int rowIndex = 0; rowIndex <= ((Grid)grid).RowDefinitions.Count; rowIndex++)
                    		{
                    			TextBox textBoxColumn1 = ((Grid)grid).Children.Cast<TextBox>().FirstOrDefault(c => Grid.GetRow(c) == rowIndex && Grid.GetColumn(c) == 0);
                    			TextBox textBoxColumn2 = ((Grid)grid).Children.Cast<TextBox>().FirstOrDefault(a => Grid.GetRow(a) == rowIndex && Grid.GetColumn(a) == 1);
                    			if (textBoxColumn1 != null && textBoxColumn2 != null)
                    			{
                    				tools[textBoxColumn1.Text] = textBoxColumn2.Text;
                    			}
                    		}
                    	}
                    }
                }
            }
            titlePage.mainToolsList.Clear();
            operationCell.Text = Count.ToString();
            StringBuilder sb = new StringBuilder(titlePage.mainToolsList.Text);
            	foreach (var item in tools)
            	{
            	if((item.Key != String.Empty) && (item.Value != String.Empty))
            	{
                	//titlePage.mainToolsList.Text += item.Key + ", шт. - " + item.Value;
                	sb.AppendLine(item.Key + ", шт. - " + item.Value);
                	titlePage.mainToolsList.Text = sb.ToString();
            	}
            	else if((item.Key != String.Empty) && (item.Value == String.Empty))
            	{
            		//titlePage.mainToolsList.Text += item.Key + Environment.NewLine;
                	sb.AppendLine(item.Key);
                	titlePage.mainToolsList.Text = sb.ToString();
            	}
            }
            sb.Clear();
            
        }

        /// <summary>
        /// Метод для добавления нового листа А4
        /// </summary>
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

        /// <summary>
        /// Метод для добавления миниатюр А4 с дочерними элементами
        /// </summary>
        private void Click_func2(object sender, RoutedEventArgs e)
        {
            violetGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(250) });
            violetGrid.Height += 250;
            violetGrid.ShowGridLines = true;
        }
        
        /// <summary>
        /// добавляет новую строку в таблицу с тех.процессом (т.е. в tableWithTechProc)
        /// </summary>
        private void AddRow(object sender, RoutedEventArgs e)
        {
        	if (sender is Button)
        	{
        		//MessageBox.Show("asdasdasdasda!!!");
        		A4_2.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(a4height) });
        		A4_2.Height += globalHeight;
        		A4_2.ShowGridLines = true;
        		A4_2.Children.Add(CreateMainTable());
        		return;
        	}
        	else
        	{
        	//MessageBox.Show("asdasdasdasda!!!");
            tableWithTechProc.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(18) });
            tableWithTechProc.Height += 18;
            //tableWithTechProc.ShowGridLines = true;
            int stackpanelNameIndex = 1;
            StackPanel newStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Background = Brushes.Red
                IsEnabled = true,
                Name = "stackPanel" + stackpanelNameIndex
            };

            #region  создание textBox'ов для Grid в stackLayout
            TextBox textBoxSP1 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Text = "1", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,1), FontSize = 10, HorizontalContentAlignment = HorizontalAlignment.Left };            
            TextBox textBoxSP2 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Text = "2", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,1), FontSize = 10, HorizontalContentAlignment = HorizontalAlignment.Center};
            textBoxSP1.KeyDown += myTextBox_KeyDown;
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
            Grid.SetRow(newStackPanel, tableWithTechProc.RowDefinitions.Count-1);
            Grid.SetColumn(newStackPanel, 4);
            Grid.SetColumnSpan(newStackPanel, 2);
            tableWithTechProc.Children.Add(newStackPanel);
            #endregion
            
            #region создание textBox'ов в колонки 1-4,7,8
            //1
            TextBox textBox_1 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Margin = new Thickness(0,0,0,0), Text = "№", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,2), FontSize = 10 };
            Grid.SetColumn(textBox_1, 0);
            Grid.SetRow(textBox_1, tableWithTechProc.RowDefinitions.Count-1);
            tableWithTechProc.Children.Add(textBox_1);
            //2
            TextBox textBox_2 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Margin = new Thickness(0,0,0,0), Text = "Новая операция", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,2), FontSize = 10 };
            Grid.SetColumn(textBox_2, 1);
            Grid.SetRow(textBox_2, tableWithTechProc.RowDefinitions.Count-1);
            tableWithTechProc.Children.Add(textBox_2);
            //3
            TextBox textBox_3 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Margin = new Thickness(0,0,0,0), Text = "Должности", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,2), FontSize = 10 };
            Grid.SetColumn(textBox_3, 2);
            Grid.SetRow(textBox_3, tableWithTechProc.RowDefinitions.Count-1);
            tableWithTechProc.Children.Add(textBox_3);
            //4
            TextBox textBox_4 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Margin = new Thickness(0,0,0,0), Text = "", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,2), FontSize = 10 };
            Grid.SetColumn(textBox_4, 3);
            Grid.SetRow(textBox_4, tableWithTechProc.RowDefinitions.Count-1);
            tableWithTechProc.Children.Add(textBox_4);
            //7
            TextBox textBox_7 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Margin = new Thickness(0,0,0,0), Text = "", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,2), FontSize = 10 };
            Grid.SetColumn(textBox_7, 6);
            Grid.SetRow(textBox_7, tableWithTechProc.RowDefinitions.Count-1);
            tableWithTechProc.Children.Add(textBox_7);
            //8
            TextBox textBox_8 = new TextBox() { MinHeight = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Margin = new Thickness(0,0,0,0), Text = "", BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,2), FontSize = 10 };
            Grid.SetColumn(textBox_8, 7);
            Grid.SetRow(textBox_8, tableWithTechProc.RowDefinitions.Count-1);
            tableWithTechProc.Children.Add(textBox_8);
            #endregion
        	}
        }
        
        /// <summary>
        /// Метод для добавления TextBox'ов в Grid(в tackPanel которая)
        /// </summary>
        private void AddTextBox(object sender, MouseWheelEventArgs e)
        {
            //MessageBox.Show("Работает!!!");
            Grid grid = new Grid() { };
            TextBox txt1 = new TextBox() { Name = "toolsCell", FontSize = 10, Height = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0,0,0,0), BorderBrush = Brushes.Black, BorderThickness = new Thickness(1,1,1,1), HorizontalContentAlignment = HorizontalAlignment.Left };
            txt1.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(myTextBox_KeyDown));
            TextBox txt2 = new TextBox() { Name = "quantityCell", FontSize = 10, Height = 18.9, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0,0,0,0), BorderBrush = Brushes.Black, BorderThickness = new Thickness(1, 1, 1, 1), HorizontalContentAlignment = HorizontalAlignment.Center };
            txt2.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(myTextBox_KeyDown));
            
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(18.9) });
            grid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(158.7) });
            grid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(37.0) });
            grid.Children.Add(txt1);
            grid.Children.Add(txt2);
            Grid.SetColumn(txt2, 1);
            StackTools.Children.Add(grid); //добавление в существующий  StackPanel следующей Grid
        } 

        /// <summary>
        /// Метод для проверки клавиши Enter
        /// </summary>
		private void myTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				AddTextBox(sender, null);
			}
		}
		
		private void GetCursor(object sender, EventArgs e)
		{
			//Проверка в textbox'е ли каретка
			if ((sender as TextBox).IsKeyboardFocused)
			{
				string  thisBoxName = (sender as TextBox).Name;
				
				titlePage.ActualDocsGrid.Text += thisBoxName;// + Grid.GetRow(sender as TextBox);
			}
		}
		
		#region всё для создания нового листа
		private Grid CreateMainTable()
		{
            //Создание "Основные и вспомогательные производственные операции и их последовательность"
            TextBox tb_OpAndFoll = new TextBox() { FontSize=10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, Text = "Основные и вспомогательные производственные операции и их последовательность", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center };
            Grid.SetRow(tb_OpAndFoll, 0);
            Grid.SetColumn(tb_OpAndFoll, 0);
            Grid.SetColumnSpan(tb_OpAndFoll, 8);

            //Создание №п/п
            TextBox tb_PP = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255,180,180,180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, TextWrapping=TextWrapping.Wrap, Text = "№ п/п", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment=VerticalAlignment.Center };
            Grid.SetRow(tb_PP, 1);
            Grid.SetColumn(tb_PP, 0);
            Grid.SetRowSpan(tb_PP, 2);

            //Создание "Наименование операции"
            TextBox tb_OperName = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, TextWrapping = TextWrapping.Wrap, Text = "Наименование операции", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            Grid.SetRow(tb_OperName, 1);
            Grid.SetColumn(tb_OperName, 1);
            Grid.SetRowSpan(tb_OperName, 2);

            //Создание "Исполнитель"
            TextBox tb_Executor = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, TextWrapping = TextWrapping.Wrap, Text = "Исполнитель", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            Grid.SetRow(tb_Executor, 1);
            Grid.SetColumn(tb_Executor, 2);
            Grid.SetRowSpan(tb_Executor, 2);

            //Создание "Оборудование, оснастка, инструмент"
            TextBox tb_ToolToolTool = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, TextWrapping = TextWrapping.Wrap, Text = "Оборудование, оснастка, инструмент", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            Grid.SetRow(tb_ToolToolTool, 1);
            Grid.SetColumn(tb_ToolToolTool, 3);
            Grid.SetColumnSpan(tb_ToolToolTool, 3);

            //Сощдание "Позиция на схеме"
            TextBox tb_PsitionOnSchema = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, TextWrapping = TextWrapping.Wrap, Text = "Позиция на схеме", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            Grid.SetRow(tb_PsitionOnSchema, 2);
            Grid.SetColumn(tb_PsitionOnSchema, 3);

            //Создание "Наименование"
            TextBox tb_Name = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, TextWrapping = TextWrapping.Wrap, Text = "Наименование", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            Grid.SetRow(tb_Name, 2);
            Grid.SetColumn(tb_Name, 4);

            //Создание "Кол-во"
            TextBox tb_Quantites = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, TextWrapping = TextWrapping.Wrap, Text = "Кол-во", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            Grid.SetRow(tb_Quantites, 2);
            Grid.SetColumn(tb_Quantites, 5);

            //Создание "Мероприятия"
            TextBox tb_Meropr = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, TextWrapping = TextWrapping.Wrap, Text = "Мероприятия по безопасному выполнению работ", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            Grid.SetRow(tb_Meropr, 1);
            Grid.SetColumn(tb_Meropr, 6);
            Grid.SetRowSpan(tb_Meropr, 2);

            //Создание "Опасные и вредные производственные факторы"
            TextBox tb_Dangerous = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, TextWrapping = TextWrapping.Wrap, Text = "Опасные и вредные производственные факторы", Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            Grid.SetRow(tb_Dangerous, 1);
            Grid.SetColumn(tb_Dangerous, 7);
            Grid.SetRowSpan(tb_Dangerous, 2);

            //создание tb6_2
            TextBox tb6_2 = new TextBox() { FontSize = 10, Background = new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), BorderThickness = new Thickness(1, 1, 1, 1), BorderBrush = Brushes.Black, Text = "6.2 Выполнение работы", Margin = new Thickness(0,0,0,0), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center };
            Grid.SetRow(tb6_2, 4);
            Grid.SetColumn(tb6_2, 0);
            Grid.SetColumnSpan(tb6_2, 8);

            //таблица с Т.П.
            Grid headGrid = new Grid(){HorizontalAlignment = HorizontalAlignment.Stretch, ShowGridLines = true};
			headGrid.RowDefinitions.Add(new RowDefinition(){Height = new GridLength(18.9)});
			headGrid.RowDefinitions.Add(new RowDefinition(){Height = new GridLength(18.9)});
			headGrid.RowDefinitions.Add(new RowDefinition(){Height = new GridLength(1, GridUnitType.Star)});
			headGrid.RowDefinitions.Add(new RowDefinition(){Height = new GridLength(18.9)});
			headGrid.RowDefinitions.Add(new RowDefinition(){Height = new GridLength(18.9)});
            headGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(18.9) });
            headGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(30)} );
			headGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(270)} );
			headGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(124)} );
			headGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(49)} );
			headGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(158.7)} );
			headGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(37)} );
			headGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(168)} );
			headGrid.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(1, GridUnitType.Star )} );
			Grid.SetRow(headGrid, 0);
			Grid.SetColumn(headGrid, 1);
            headGrid.Children.Add(tb6_2);
            headGrid.Children.Add(tb_OpAndFoll);
            headGrid.Children.Add(tb_PP);
            headGrid.Children.Add(tb_OperName);
            headGrid.Children.Add(tb_Executor);
            headGrid.Children.Add(tb_ToolToolTool);
            headGrid.Children.Add(tb_PsitionOnSchema);
            headGrid.Children.Add(tb_Name);
            headGrid.Children.Add(tb_Quantites);
            headGrid.Children.Add(tb_Meropr);
            headGrid.Children.Add(tb_Dangerous);


            //Создание кнопки "Добавить новый лист"
            Button button_AddNewList = new Button() { Height = 50, Width = 152, VerticalAlignment=VerticalAlignment.Bottom, Content = "Добавить Лист", Background = new SolidColorBrush(Colors.Bisque), BorderThickness = new Thickness(0,5,0,5), BorderBrush = new SolidColorBrush(Color.FromArgb(255, 180, 120, 120)) };
            Grid.SetRow(button_AddNewList, A4_2.RowDefinitions.Count);
            button_AddNewList.Click += AddRow;
            A4_2.Children.Add(button_AddNewList);

            //Заполнение Нового листа
            Grid mainGrid1 = new Grid()
			{
				Name="mainGrid1",
				Background = new SolidColorBrush(Colors.White),
				//Grid.Column="0"
				//Grid.Row="0"
				Margin=new Thickness(50,50,50,50),
				ShowGridLines = true				
			};
			
			mainGrid1.Children.Add(headGrid);
			
			mainGrid1.RowDefinitions.Add( new RowDefinition(){ Height = new GridLength(1, GridUnitType.Auto) });
			mainGrid1.RowDefinitions.Add( new RowDefinition() );
			mainGrid1.ColumnDefinitions.Add( new ColumnDefinition(){ Width = new GridLength(30)} );
			mainGrid1.ColumnDefinitions.Add( new ColumnDefinition() );
			Grid.SetRow(mainGrid1, A4_2.RowDefinitions.Count-1);
			return mainGrid1;
		}
		#endregion
    }
}
