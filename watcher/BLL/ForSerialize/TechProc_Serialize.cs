using System.Collections.Generic;

namespace watcher.BLL.ForSerialize
{
    public class TechProc_Serialize
    {
        public string TechProcTableName { get; set; } = "TechProcTable";
        public int TechProcTable_Row { get; set; }
        public int TechProcTable_Column { get; set; }
        public List<TextBox_Serialize>? TextBox_Serialize { get; set; }
        public List<StackPanel_Serialize>? StackPanel_Serialize { get; set; }
    }
}
