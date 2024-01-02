using System.Collections.Generic;

namespace watcher.BLL.ForSerialize;

public class StackPanel_Serialize
{
    public string? StackPanelName { get; set; }
    public int StackPanelRowInTechProcGrid { get; set; }
    public int StackPanelColumnInTechProcGrid { get; set; }
    public int StackPanelSpanColumn { get; set; }
    public List<GridInStackPanel_Serialize>? gridsInStackPanel { get; set; } = new List<GridInStackPanel_Serialize>();
}
