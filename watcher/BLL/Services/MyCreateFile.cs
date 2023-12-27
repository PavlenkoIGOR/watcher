using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace watcher.BLL.Services;

internal static class MyCreateFile
{
    internal static string[] paths = { @"d:\","elems.txt"};
    internal static string pathFull = "";
    internal static void MyCreateFileMethod(List<UIElement> list)
    {
        pathFull = System.IO.Path.Combine(paths);
        if (!File.Exists(pathFull))
        {
            File.Create(pathFull);
        }

        using (FileStream fs = new FileStream(pathFull, FileMode.Append, FileAccess.Write, FileShare.None))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (UIElement element in list)
                {
                    sw.WriteLine($"{element.GetType()}");
                }
            }
        }


    }
}
