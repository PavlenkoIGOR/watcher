using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace watcher.BLL.Services;

internal static class MyTraverse
{
    internal static void TraverseElements(DependencyObject parent, List<UIElement> elements)
    {
        int count = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < count; i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);
            if (child is UIElement uiElement)
            {
                elements.Add(uiElement);
                if (child is TabItem || child is TabControl)
                {
                    TraverseElements(child, elements);
                }
            }
        }
    }
}
