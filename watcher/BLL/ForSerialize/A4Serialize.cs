using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watcher.BLL.ForSerialize;

public class A4Serialize
{
    public string A4Name { get; set; }
    public int A4Rows { get; set; }
    public List<Grid2x2_Serialize>? grid2X2_Serialize { get; set; }

}
