using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watcher.BLL.ForSerialize
{
    public class TextBox_Serialize
    {
        public byte TextBoxRow { get; set; }
        public byte TextBoxColumn { get; set; }
        public byte TextBoxColumnSpan { get; set; }
        public byte TextBoxRowSpan { get; set; }
        public string TextBox_Text { get; set; }
    }
}
