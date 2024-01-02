using System.Collections.Generic;

namespace watcher.BLL.ForSerialize;

public class TechProc_Serialize
{
    public string TechProcTableName { get; set; } = "TechProcTable";
    public int TechProcTable_RowInA4 { get; set; }
    public int TechProcTable_ColumnInA4 { get; set; }
    public List<TextBox_Serialize>? TextBoxsList_Serialize { get; set; } = new List<TextBox_Serialize>();
    public List<StackPanel_Serialize>? StackPanelsList_Serialize { get; set; } = new List<StackPanel_Serialize>();
}
