using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watcher.BLL.ForSerialize
{
    public class GridInStackPanel_Serialize
    {
        public string? GridInStackPanelName { get; set; }
        public sbyte ? GridInStackPanelRow { get; set; }
        public sbyte ? GridInStackPanelColumn { get; set; }

        public List<TextBox_Serialize> TextBoxsInStackPanel{ get; set; }
    }
}
