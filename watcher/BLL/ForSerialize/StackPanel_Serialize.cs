using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watcher.BLL.ForSerialize
{
    public class StackPanel_Serialize
    {
        public string StackPanelName { get; set; }
        public sbyte StackPanelRow {  get; set; }
        public sbyte StackPanelColumn { get; set; }
        public GridInStackPanel_Serialize gridInStackPanel {  get; set; } 
    }
}
