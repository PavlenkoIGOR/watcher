using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watcher.BLL.ForSerialize;

public class TextBox_Serialize
{
    public int TextBoxRow { get; set; }
    public int TextBoxColumn { get; set; }
    public int? TextBoxColumnSpan { get; set; }
    public int? TextBoxRowSpan { get; set; }
    public string? TextBox_Text { get; set; }
    public string? Tag { get; set; }
}



