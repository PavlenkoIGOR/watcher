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
using watcher.BLL;

namespace watcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
    		//для работы с текстбоксами "NumOfRow"		
		List <TextBox> NumsOfRowsList = new List<TextBox>();
		int countTB=1;
        Grid A4;
        SheetAndSheetsGridCreateClass _sheetsAndSheet = new SheetAndSheetsGridCreateClass();
        StackCreatingClass _stackCreatingClass = new StackCreatingClass();
        TechProcGridCreatingClass _fotTechProcTab = new TechProcGridCreatingClass();
        TitlePage titlePage;
        A4CreatingClass _A4CreatingClass = new A4CreatingClass();
        Creating2x2GridClass _creating2x2GridClass = new Creating2x2GridClass();
        AddA4DeleteA4GridClass _addA4DeleteA4Class = new AddA4DeleteA4GridClass();

        private double globalHeight = 793.7d;
        private double a4height = 793.7d;
        public MainWindow()
        {
            //_A4CreatingClass = new A4CreatingClass();
            //_sheetsAndSheet = new SheetAndSheetsGridCreateClass();
            //_stackCreatingClass = new StackCreatingClass();
            //_fotTechProcTab = new TechProcGridCreatingClass(_stackCreatingClass);
            titlePage = new TitlePage();
            //_creating2x2GridClass = new Creating2x2GridClass();

            InitializeComponent();
            //forTitlePage.Source = new Uri("TitlePage.xaml", UriKind.Relative); //эта строка подключает свой Page. В данный момент не нужна т.к. подключается сейчас через InputPage().
            InputPage();
            InsertA4IntoScrollViewer();
        }
		/// <summary>
	/// метод для подключения своего Frame
	/// </summary>
	private void InputPage()
        {
            //MainWindow mainWindow = new MainWindow();
            Frame? mainFrame = this.FindName("forTitlePage") as Frame; // Найдите элемент Frame в главном окне
            mainFrame.Navigate(titlePage); // Загружаете вашу страницу во Frame
            this.Show(); // Отображаете главное окно
        }


        //перебор элементов !!!!!!!!!!!!!!!!!!!!Этот метод можно использовать для сериализации всех данных отрисовки в иерархии визуального объекта.
        /// <summary>
        /// Метод, устанавливающий номера строк с ТП
        /// </summary>
        ///[Serializable]
		private void RecursivelyProcessVisualTree(DependencyObject element)
		{
			// Проверка, является ли элемент контейнером
			if (element is Visual)
			{
				// Рекурсивный обход дочерних элементов
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
				{					
					if((element is TextBox) && (element as TextBox).Name == "NumOfRow")
					{						
						countTB++;	
						(element as TextBox).Text = countTB.ToString();
						MessageBox.Show("текстбокс с именем 'NumOfRow' найден");
					}
					else
					{
						DependencyObject child = VisualTreeHelper.GetChild(element, i);
						RecursivelyProcessVisualTree(child);
					}					
				}
			}
		}

        /// <summary>
        /// Метод вставки А4 во вкладку
        /// </summary>
        private void InsertA4IntoScrollViewer()
        {
           
            //создание листа А4
            A4 = _A4CreatingClass.CreatA4();

            //создание таблицы с тех.процессом
            Grid headGrid = _fotTechProcTab.CreateMainTable();
            //_fotTechProcTab.CreateTextBox1_2_3_4_7_8(s,r,headGrid);

            //создание таблицы с количеством листов
            Grid sheetAndSheets = _sheetsAndSheet.CreateSheetAndSheetsGrid();

            //создание основной сетки 2х2
            Grid grid2x2 = _creating2x2GridClass.Creating2x2Grid();
            Grid.SetRow(grid2x2, A4.RowDefinitions.Count - 1);
            Grid.SetColumn(grid2x2, 0);
            grid2x2.Children.Add(headGrid);
            grid2x2.Children.Add(sheetAndSheets);

            //cсоздание таблицы с кнопками "Добавить после/удалить после"
            Grid addA4DeleteA4 = _addA4DeleteA4Class.AddA4DeleteA4(A4);
            Grid.SetRow(addA4DeleteA4, A4.RowDefinitions.Count - 1);
            Grid.SetColumn(addA4DeleteA4, A4.ColumnDefinitions.Count - 1);

            //создание StackPanel для сетки с тех.процессом
            StackPanel stackTP = _stackCreatingClass.CreateStackPanelIntoTeckProcesstable();
            Grid.SetRow(stackTP, headGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(stackTP, 4);
            Grid.SetColumnSpan(stackTP, 2);
                        
            headGrid.Children.Add(stackTP);
           
            A4.Children.Add(grid2x2);
            A4.Children.Add(addA4DeleteA4);

            ScrollViewerForTabs.Content = A4; //вставка А4
                 //вставка в А4 таблицы-разметка 2х2
        }

        private void Renew(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> tools = new Dictionary<string, string>();
            string textBox1Value = String.Empty;
            string textBox2Value = String.Empty;
            int Count = 0;


            for (int rowIndexM = 0; rowIndexM < A4.RowDefinitions.Count; rowIndexM++) // перебор всех листов А4 (т.е. всех строк в таблице А4_2)
            {
                foreach (UIElement elemenTabMain in A4.Children) //перебор элементов//********* ((Grid)A4_2.Children[rowIndexM]).Children
                {
                    if (elemenTabMain is Grid) //здесь таблица mainGrid (в ней уже надо искать таблицу tableWithTechProc)
                    {
                        foreach (var elementTP in (elemenTabMain as Grid).Children)
                        {
                            if (elementTP is Grid)
                            {
                                foreach (UIElement element in (elementTP as Grid).Children) //перечисление всех дочерних элементов у tableWithTechProc таблицы
                                {
                                    if (element is StackPanel)
                                    {
                                        Count++;
                                        foreach (UIElement grid in ((StackPanel)element).Children) //переборка всех гридов в StackPanel
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
                            }
                        }
                    }
                }
		countTB = 0;
  		RecursivelyProcessVisualTree(A4);
            }
            titlePage.mainToolsList.Clear();
            StringBuilder sb = new StringBuilder(titlePage.mainToolsList.Text);
            foreach (var item in tools)
            {
                if ((item.Key != String.Empty) && (item.Value != String.Empty))
                {
                    sb.AppendLine(item.Key + ", шт. - " + item.Value);
                    titlePage.mainToolsList.Text = sb.ToString();
                }
                else if ((item.Key != String.Empty) && (item.Value == String.Empty))
                {
                    sb.AppendLine(item.Key);
                    titlePage.mainToolsList.Text = sb.ToString();
                }
            }
            sb.Clear();
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
		/// Метод проверки в TextBox'е ли каретка
		/// </summary>
		private void GetCursor(object sender, EventArgs e)
		{
			//Проверка в textbox'е ли каретка
			if ((sender as TextBox).IsKeyboardFocused)
			{
				string  thisBoxName = (sender as TextBox).Name;
				
				titlePage.ActualDocsGrid.Text += thisBoxName;
			}
		}


        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
        	// Выполните ваше действие при получении фокуса клавиатуры, например, задайте команду для вызова
        	var focusedElement = Keyboard.FocusedElement as FrameworkElement;
        	if (focusedElement != null)
        	{
        		string elementName = focusedElement.Name;
        		// Вы можете использовать elementName в вашем коде
        	}
        }
                
        ///<summary>
        /// метод отправки на печать
        /// </summary>
        private void GoPrinting(object sender, RoutedEventArgs e)
        {
            TabItem tabForPrint = execProcTab;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(tabForPrint.Content as Visual, "Печать содержимого TabItem");
            }
        }

        
    }

}
