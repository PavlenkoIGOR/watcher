using System.Collections.Generic;

namespace watcher.BLL.ForSerialize;

public class GridInStackPanel_Serialize
{
    public string? GridInStackPanelName { get; set; }
    public int? GridsInStackPanelRow { get; set; }
    public List<TextBox_Serialize> TextBoxsInStackPanel{ get; set; }
}
