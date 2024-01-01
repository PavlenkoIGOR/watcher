using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watcher.BLL.ForSerialize;

public class Grid2x2_Serialize
{
    public string NameGrid { get; set; }
    public int RowQuantity { get; set; }
    public int ColumnQuantity { get; set; }
    public TechProc_Serialize techProc_Serialize { get; set; }
    public SheetSheetsGrid_Serialize sheetSheetsGrid_Serialize { get; set;}
    public TextBox_Serialize textBox_Serialize { get; set; }
}
