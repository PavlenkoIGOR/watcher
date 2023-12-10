using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Xml.Linq;
using watcher.BLL;
using WRD = Microsoft.Office.Interop.Word;

namespace watcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        //для работы с текстбоксами "NumOfRow"		
        List<TextBox> NumsOfRowsList = new List<TextBox>();
        int countTB = 1;
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
            titlePage = new TitlePage();

            InitializeComponent();
            //forTitlePage.Source = new Uri("TitlePage.xaml", UriKind.Relative); //эта строка подключает свой Page. В данный момент не нужна т.к. подключается сейчас через InputPage().
            InputPage();
            InsertA4IntoScrollViewer();
            SaveAsWRD.Click += (s, e) => SaveGridToWord(_fotTechProcTab.CreateMainTable());
            SaveMy.Click += (s, e) => RecursivelyProcessVisualTree();
        }
        /// <summary>
        /// метод для подключения своего Frame
        /// </summary>
        private void InputPage()
        {
            //MainWindow mainWindow = new MainWindow();
            Frame mainFrame = this.FindName("forTitlePage") as Frame; // Найти элемент Frame в главном окне
            mainFrame.Navigate(titlePage); // Загрузка своей страницы во Frame
            this.Show(); // Отображение главного окна
        }


        //перебор элементов !!!!!!!!!!!!!!!!!!!!Этот метод можно использовать для сериализации всех данных отрисовки в иерархии визуального объекта.
        /// <summary>
        /// Метод, устанавливающий номера строк с ТП
        /// </summary>
         List < UIElement > elements = new List< UIElement >();
        public  void RecursivelyProcessVisualTree()
        {
            TabControl? tabControl = this.FindName("myTabControl") as TabControl;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(tabControl); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(tabControl, i);
                if (child is Visual)
                {
                    // Производим дальнейшие действия с дочерним элементом
                    Visual visualChild = child as Visual;
                }
            }
        }

        private static void Traverse(UIElement element)
        {
            if (element is Panel panel)
            {
                foreach (UIElement child in panel.Children)
                {
                    Traverse(child);
                }
            }
        }

        private void CreateTXTse(Object sender, EventArgs e)
        {

            foreach (var element in elements)
            {
                CreateTXT(element);
            }
        }
        #region сегодня это не нужно
        /// <summary>
        /// Метод вставки А4 во вкладку
        /// </summary>
        private void InsertA4IntoScrollViewer()
        {

            //создание листа А4
            A4 = _A4CreatingClass.CreatA4();
            A4.Name = "A4";

            //создание таблицы с тех.процессом
            Grid headGrid = _fotTechProcTab.CreateMainTable();


            //_fotTechProcTab.CreateTextBox1_2_3_4_7_8(s,r,headGrid);
            //создание таблицы с количеством листов
            Grid sheetAndSheets = _sheetsAndSheet.CreateSheetAndSheetsGrid();

            //создание основной сетки 2х2
            Grid grid2x2 = _creating2x2GridClass.Creating2x2Grid();
            grid2x2.Name = "grid2x2";
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
            }
            countTB = 0;
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
                string thisBoxName = (sender as TextBox).Name;

                titlePage.ActualDocsGrid.Text += thisBoxName;
            }
        }


        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            // Выполнить нужное действие при получении фокуса клавиатуры, например, задать команду для вызова
            var focusedElement = Keyboard.FocusedElement as FrameworkElement;
            if (focusedElement != null)
            {
                string elementName = focusedElement.Name;
                // можно использовать elementName в коде
            }
        }
        #endregion

        ///<summary>
        /// метод отправки на печать
        /// </summary>
        private void GoPrinting(object sender, RoutedEventArgs e)
        {
            DocumentViewer docViewer = new DocumentViewer();
            //выбор элемента для печати
            TabItem tabForPrint = execProcTab;
            //создание окна печати
            PrintDialog printDialog = new PrintDialog();

            printDialog.PageRangeSelection = PageRangeSelection.AllPages;
            printDialog.UserPageRangeEnabled = true;

            FlowDocument doc = (FlowDocument)docViewer.Document;

            if (printDialog.ShowDialog() == true)
            {
                // Сохранить все имеющиеся настройки
                double pageHeight = doc.PageHeight;
                double pageWidth = doc.PageWidth;
                Thickness pagePadding = doc.PagePadding;
                double columnGap = doc.ColumnGap;
                double columnWidth = doc.ColumnWidth;

                // Привести страницу FlowDocument в соответствие с печатной страницей
                doc.PageHeight = printDialog.PrintableAreaHeight;
                doc.PageWidth = printDialog.PrintableAreaWidth;
                doc.PagePadding = new Thickness(0);

                printDialog.PrintDocument(
                    ((IDocumentPaginatorSource)doc).DocumentPaginator, "A Flow Document");




                // Устанавливаем ориентацию печати
                PrintTicket ticket = printDialog.PrintTicket;
                PageOrientation landscape = (tabForPrint.ActualWidth > tabForPrint.ActualHeight) ? PageOrientation.Landscape : PageOrientation.Portrait;
                ticket.PageOrientation = landscape;
                // Разрешаем пользователю выбирать ориентацию печати в окне печати
                printDialog.UserPageRangeEnabled = true;
                printDialog.PrintVisual(tabForPrint.Content as Visual, "Печать содержимого TabItem");
            }
            else
            {
                return;
            }
        }

        private void SaveGridToWord(Grid grid)
        {
            // Создание нового экземпляра приложения Word
            WRD.Application wordApp = new WRD.Application();
            // Создание нового документа
            WRD.Document document = wordApp.Documents.Add();

            // Получение активного раздела документа
            WRD.Section section = document.ActiveWindow.Selection.Sections.Add();

            // Создаем таблицу с теми же размерами, что и Grid
            WRD.Table wordTable = document.Tables.Add(document.Range(), grid.RowDefinitions.Count, grid.ColumnDefinitions.Count);

            // Проходим через каждую ячейку Grid
            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count; j++)
                {
                    // Получаем TextBox в ячейке Grid
                    TextBox textBox = grid.Children[j] as TextBox;

                    // Вставляем текст из TextBox в ячейку таблицы Word
                    wordTable.Cell(i, j).Range.Text = textBox.Text;
                }
            }

            // Обход элементов внутри Grid
            foreach (var element in grid.Children)
            {
                if (element is TextBox)
                {
                    // Получение текста из TextBox
                    string text = (element as TextBox).Text;

                    // Добавление текста в документ
                    var paragraph = section.Range.Paragraphs.Add();
                    paragraph.Range.Text = text;
                }
            }

            //        	// Сохранение документа
            //        	string filePath = "путь_к_файлу.docx";
            //document.SaveAs(filePath);
            // Сохраняем документ Word в нужном формате
            document.SaveAs2(@"D:\VS\qwerty.docx", (object)WRD.WdSaveFormat.wdFormatDocumentDefault);

            // Закрытие документа и приложения Word
            document.Close();
            wordApp.Quit();
        }

        private void CreateTXT(DependencyObject dependencyObject)
        {
            string[] path = { @"d:\", "Text1hw1_13.txt" };
            string filePath = Path.Combine(path);

            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"{dependencyObject.GetType()}");
                }
            }

        }
    }

}
