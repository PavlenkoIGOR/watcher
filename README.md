# watcher
для метода-конструктора в основном файле:
SaveAsWRD.Click += (s,e) => SaveGridToWord(_fotTechProcTab.CreateMainTable());

для тела класса MainWindow:
```C#
        private void GoPrinting(object sender, RoutedEventArgs e)
        {
            TabItem tabForPrint = execProcTab;
            PrintDialog printDialog = new PrintDialog();
            
            if (printDialog.ShowDialog() == true)
            {
            	
            	printDialog.PrintVisual(tabForPrint.Content as Visual, "Печать содержимого TabItem");            	
            }
        }
        
        private void SaveAsFile(object sender, RoutedEventArgs e)
        {
        	SaveFileDialog sfd = new SaveFileDialog();
        	sfd.Filter = "Документы Word (*.docx)|*.docx|Документы Word 97-2003 (*.doc)|*.doc|Все файлы (*.*)|*.*";
        	sfd.FileName = "document.docx";
        	if(sfd.ShowDialog() == true)
        	{
        		string filePath = sfd.FileName;
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
        			wordTable.Cell(i , j).Range.Text = textBox.Text;
        		}
        	}
        	
        	// Обход элементов внутри Grid
        	foreach (var element in grid.Children)
        	{
        		if (element is TextBox )
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
        	document.SaveAs2(@"D:\!!!\qwerty.docx", (object)WRD.WdSaveFormat.wdFormatDocumentDefault);
        	
        	// Закрытие документа и приложения Word
        	document.Close();
        	wordApp.Quit();
        }
```
