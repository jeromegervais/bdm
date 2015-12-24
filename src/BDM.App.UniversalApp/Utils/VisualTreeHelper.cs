using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace BDM.App.UniversalApp.Utils
{
    public static class VisualTreeHelper
    {
        public static TAncestor FindAncestor<TAncestor>(this DependencyObject dependencyObject) where TAncestor : DependencyObject
        {
            while (!(dependencyObject is TAncestor))
            {
                var parent = Windows.UI.Xaml.Media.VisualTreeHelper.GetParent(dependencyObject);
                if (parent == null)
                    return null;

                dependencyObject = parent;
            }

            return (TAncestor)dependencyObject;
        }

        public static IEnumerable<TChild> FindChild<TChild>(this DependencyObject dependencyObject) where TChild : DependencyObject
        {
            for (var i = 0; i < Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                var child = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(dependencyObject, i);
                if(child is TChild)
                    yield return (TChild)child;

                foreach (var grandChild in child.FindChild<TChild>())
                {
                    yield return grandChild;
                }
            }
        }
    }
}
