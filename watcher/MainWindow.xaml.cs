using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using watcher.BLL;
using watcher.BLL.ForSerialize;
using watcher.BLL.Services;
using WRD = Microsoft.Office.Interop.Word;

namespace watcher;

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
    /// метод для подключения своего Frame. В данном случае метод для вставки титульного листа в первую вкладку
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
    /// Метод, устанавливающий номера строк с ТП - где он?
    /// </summary>
    List<UIElement> elementsMain = new List<UIElement>();
    public void RecursivelyProcessVisualTree()
    {
        TabControl? tabControl = this.FindName("myTabControl") as TabControl;
        if (tabControl != null)
        {
            MyTraverse.TraverseElements(tabControl, elementsMain);
        }

        MyTraverse.TraverseElements(tabControl, elementsMain);
        MyCreateFile.MyCreateFileMethod(elementsMain);

    }

    #region
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
                                          //A4.RegisterName("headGrid", headGrid);
        A4.RegisterName("grid2x2", grid2x2);
        headGrid.Name = "headGrid";
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
                    foreach (UIElement elementTP in (elemenTabMain as Grid).Children)
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

        Serializer();

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
        PageContent pageContent = new PageContent();
        FixedPage fixedPage = new FixedPage();



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

    private void Testing_Button_Click(object sender, RoutedEventArgs e)
    {
        Grid headGrid = (Grid)myTabControl.FindName("headGrid");
        foreach (var child in headGrid.Children.OfType<TextBox>().ToList())
        {
            headGrid.Children.Remove(child);
        }
    }

    /// <summary>
    /// Метод для сериализации
    /// </summary>
    /// <returns></returns>
    TP_TabSerialize Serializer()
    {

        TextBox_Serialize textBoxInTProc_Serialize = new TextBox_Serialize();
        StackPanel_Serialize stackPanel_Serialize = new StackPanel_Serialize();

        GridInStackPanel_Serialize gridInStackPanel_Serialize = new GridInStackPanel_Serialize();
        Grid2x2_Serialize grid2X2_Serialize = new Grid2x2_Serialize();
        A4Serialize a4Serialize = new A4Serialize();
        TP_TabSerialize tP_TabSerialize = new TP_TabSerialize();

        List<Grid2x2_Serialize> grid2X2_Ser = new List<Grid2x2_Serialize>();
        //foreach (var item in A4.Children)
        //{
        //    if ((item as Grid).Name == "mainGrid")
        //    {
        //        Grid2x2_Serialize grid2X2_Serialize_Inner = new Grid2x2_Serialize();
        //        TextBox_Serialize textBox_Serialize = new TextBox_Serialize();
        //        TechProc_Serialize techProc_Serialize = new TechProc_Serialize();
        //        SheetSheetsGrid_Serialize sheetSheetsGrid_Serialize = new SheetSheetsGrid_Serialize();

        //        for (int i = 0; i < (item as Grid).Children.Count; i++)
        //        {

        //            if (i == 0)
        //            {
        //                textBox_Serialize.TextBoxRow = Grid.GetRow((item as Grid).Children[0]);
        //                textBox_Serialize.TextBoxColumn = Grid.GetColumn((item as Grid).Children[0]);
        //                textBox_Serialize.TextBox_Text = ((TextBox)(item as Grid).Children[0]).Text;
        //            }
        //            if (i == 1)
        //            {
        //                foreach (var child in ((Grid)(item as Grid).Children[1]).Children)
        //                {
        //                    if (child is TextBox)
        //                    {
        //                        techProc_Serialize.TextBoxsList_Serialize.Add(new TextBox_Serialize()
        //                        {
        //                            TextBoxRow = Grid.GetRow((TextBox)child),
        //                            TextBoxColumn = Grid.GetColumn((TextBox)child),
        //                            TextBoxColumnSpan = Grid.GetColumnSpan((TextBox)child),
        //                            TextBoxRowSpan = Grid.GetRowSpan((TextBox)child),
        //                            TextBox_Text = ((TextBox)child).Text
        //                        });
        //                    }
        //                    if (child is StackPanel)
        //                    {
        //                        List<GridInStackPanel_Serialize> gridInStackPanel_Ser = new List<GridInStackPanel_Serialize>();
        //                        int count = 0;
        //                        foreach (var childSP in (child as StackPanel).Children)
        //                        {
        //                            if (childSP is Grid)
        //                            {
        //                                List<TextBox_Serialize> tbSer = new List<TextBox_Serialize>();
        //                                foreach (var tb in (childSP as Grid).Children)
        //                                {
        //                                    tbSer.Add(new TextBox_Serialize()
        //                                    {
        //                                        TextBoxColumn = Grid.GetColumn((TextBox)tb),
        //                                        TextBoxRow = Grid.GetRowSpan((TextBox)tb),
        //                                        TextBox_Text = ((TextBox)tb).Text
        //                                    });
        //                                }
        //                                gridInStackPanel_Ser.Add(new GridInStackPanel_Serialize()
        //                                {
        //                                    GridsInStackPanelRow = count++,
        //                                    TextBoxsInStackPanel = tbSer
        //                                });
        //                            }
        //                        }
        //                        techProc_Serialize.StackPanelsList_Serialize.Add(new StackPanel_Serialize
        //                        {
        //                            StackPanelRowInTechProcGrid = Grid.GetRow((StackPanel)child),
        //                            StackPanelColumnInTechProcGrid = Grid.GetColumn((StackPanel)child),
        //                            StackPanelSpanColumn = Grid.GetColumnSpan((StackPanel)child),
        //                            gridsInStackPanel = gridInStackPanel_Ser
        //                        });
        //                    }
        //                }
        //            }
        //            if (i == 2)
        //            {
        //                sheetSheetsGrid_Serialize.Rows = ((Grid)(item as Grid).Children[2]).RowDefinitions.Count;
        //                sheetSheetsGrid_Serialize.Rows = ((Grid)(item as Grid).Children[2]).ColumnDefinitions.Count;
        //                sheetSheetsGrid_Serialize.SheetNum = 55;
        //                sheetSheetsGrid_Serialize.SheetsQuantity = 101;
        //            }

        //        }
        //        grid2X2_Ser.Add(new Grid2x2_Serialize()
        //        {
        //            textBox_Serialize = textBox_Serialize,
        //            techProc_Serialize = techProc_Serialize,
        //            sheetSheetsGrid_Serialize = sheetSheetsGrid_Serialize
        //        });
        //    }
        //    if ((item as Grid).Name == "AddA4DeleteA4")
        //    {
        //        MessageBox.Show("Это AddA4DeleteA4GridClass");
        //    }
        //}
        //tP_TabSerialize.A4Serialize = a4Serialize;
        //tP_TabSerialize.A4Serialize.A4Rows = A4.RowDefinitions.Count;
        //tP_TabSerialize.A4Serialize.grid2X2_Serialize = grid2X2_Ser;


        // сохранение данных
        using (FileStream fs = new FileStream(@"D:/Watcher.json", FileMode.Create, FileAccess.Write))
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            JsonSerializer.Serialize<TP_TabSerialize>(fs, tP_TabSerialize, options);
            fs.Close();
        }
        return new TP_TabSerialize();
    }
}
