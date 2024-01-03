using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watcher.BLL.ForSerialize;

public class SheetSheetsGrid_Serialize
{
    public string? SheetAndSheetsGridName { get; set; }
    public int RowIn2x2 { get; set; }
    public int ColumnIn2x2 { get;set; }
    public int Rows { get; set; }
    public int Column { get; set; }
    public int? SheetNum { get; set;}
    public int? SheetsQuantity { get; set;}
}
