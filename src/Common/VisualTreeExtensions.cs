using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Zhai.FamilTheme.Common
{
    /// <summary>
    /// 查找控件扩展方法
    /// </summary>
    public static class VisualTreeExtensions
    {
        /// <summary>
        /// 在可视化树上查找给指定项的子项
        /// </summary>
        public static T FindVisualChild<T>(this DependencyObject parent)
            where T : DependencyObject
        {
            if (null != parent)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child != null && child is T t)
                        return t;

                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
        }

        /// <summary>
        /// 在可视化树上查找给指定项的子项
        /// </summary>
        public static T FindVisualChildByName<T>(this DependencyObject parent, string name)
            where T : FrameworkElement
        {
            if (null != parent)
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child != null && child is T t && (child as T).Name.Equals(name))
                        return t;

                    var childOfChild = FindVisualChildByName<T>(child, name);
                    if (childOfChild != null && childOfChild is T && childOfChild.Name.Equals(name))
                        return childOfChild;
                }

            return null;
        }

        /// <summary>
        /// 在可视化树上查找给指定项的父项
        /// </summary>
        private static T FindVisualParent<T>(this DependencyObject child)
            where T : DependencyObject
        {
            //get parent item
            var parentObject = GetVisualParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            var parent = parentObject as T;

            return parent ?? FindVisualParent<T>(parentObject);
        }

        /// <summary>
        /// 根据控件名称，查找父控件 
        /// name为空时，查找指定类型的父控件
        /// </summary>
        public static T FindVisualParentByName<T>(this DependencyObject child, string name)
                where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                if (parent is T t && (t.Name == name || string.IsNullOrEmpty(name)))
                    return t;
                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }



        /// <summary>
        /// 根据控件名称，查找子控件
        /// elementName为空时，查找指定类型的子控件
        /// </summary>
        public static T FindLogicalChild<T>(this DependencyObject parent, string elementName)
            where T : FrameworkElement
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T t && t.Name == elementName || string.IsNullOrEmpty(elementName))
                    return (T)child;

                var grandChild = FindLogicalChild<T>(child, elementName);

                if (grandChild != null) return grandChild;
            }

            return null;
        }

        /// <summary>
        /// 根据控件名称查找子控件集合
        /// name为空时，查找指定类型的所有子控件
        /// </summary>
        public static List<T> FindLogicalChilds<T>(this DependencyObject parent, string elementName)
            where T : FrameworkElement
        {
            var childList = new List<T>();

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T t && t.Name == elementName || string.IsNullOrEmpty(elementName))
                {
                    childList.Add((T)child);
                }
                else
                {
                    var grandChildList = FindLogicalChilds<T>(child, elementName);

                    if (grandChildList != null)
                        childList.AddRange(grandChildList);
                }
            }

            return childList;
        }



        /// <summary>
        /// 在可视化树中查找给定名称的子项
        /// </summary>
        public static T FindChild<T>(this DependencyObject parent, string name)
            where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                if (!(child is T t))
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, name);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(name))
                {
                    // If the child's name is set for search
                    if (child is FrameworkElement frameworkElement && frameworkElement.Name == name)
                    {
                        // if the child's name is of the request name
                        foundChild = t;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = t;
                    break;
                }
            }

            return foundChild;
        }

        /// <summary>
        /// 这将搜索指定类型的子级
        /// 搜索是按层次进行的，广度优先（而不是深度优先）。
        /// </summary>
        public static T FindChild<T>(this DependencyObject parent, Func<T, bool> additionalCheck = null)
            where T : DependencyObject
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            T foundChild;

            for (var index = 0; index < childrenCount; index++)
            {
                foundChild = VisualTreeHelper.GetChild(parent, index) as T;

                if (foundChild != null)
                {
                    if (additionalCheck == null)
                        return foundChild;
                    else if (additionalCheck(foundChild))
                        return foundChild;
                }
            }

            for (var index = 0; index < childrenCount; index++)
            {
                foundChild = FindChild(VisualTreeHelper.GetChild(parent, index), additionalCheck);

                if (foundChild != null)
                    return foundChild;
            }

            return null;
        }

        /// <summary>
        /// 分析可视树或逻辑树，以查找作为源项后代的给定类型的所有元素。
        /// </summary>
        public static IEnumerable<T> FindChilds<T>(this DependencyObject parent)
            where T : DependencyObject
        {
            if (parent != null)
            {
                var childs = GetChilds(parent, false);

                foreach (var child in childs)
                {
                    //analyze if children match the requested type
                    if (child != null && child is T t) yield return t;

                    //recurse tree
                    foreach (var descendant in FindChilds<T>(child))
                        yield return descendant;
                }
            }
        }

        /// <summary>
        /// 分析逻辑树，以查找作为源项后代的给定类型的所有元素。
        /// </summary>
        public static IEnumerable<T> FindVisualChilds<T>(this DependencyObject parent)
            where T : DependencyObject
        {
            if (parent != null)
            {
                var childs = GetChilds(parent, true);

                foreach (var child in childs)
                {
                    //analyze if children match the requested type
                    if (child != null && child is T t) yield return t;

                    //recurse tree
                    foreach (var descendant in FindVisualChilds<T>(child))
                        yield return descendant;
                }
            }
        }



        /// <summary>
        /// 搜索指定类型的父级
        /// </summary>
        /// <typeparam name="T">The type of the element to find</typeparam>
        /// <param name="startingObject">The node where the search begins. This element is not checked.</param>
        /// <returns>Returns the found element. Null if nothing is found.</returns>
        public static T FindParent<T>(this DependencyObject child) 
            where T : DependencyObject
        {
            return FindParent<T>(child, false, null);
        }

        /// <summary>
        /// 搜索指定类型的父级
        /// </summary>
        /// <typeparam name="T">The type of the element to find</typeparam>
        /// <param name="startingObject">The node where the search begins.</param>
        /// <param name="checkStartingObject">Should the specified startingObject be checked first.</param>
        /// <returns>Returns the found element. Null if nothing is found.</returns>
        public static T FindParent<T>(this DependencyObject child, bool isCheckedChild) 
            where T : DependencyObject
        {
            return FindParent<T>(child, isCheckedChild, null);
        }

        /// <summary>
        /// 搜索指定类型的父级
        /// </summary>
        public static T FindParent<T>(this DependencyObject child, bool isCheckedChild, Func<T, bool> additionalCheckFunc) 
            where T : DependencyObject
        {
            T foundElement;

            var parent = isCheckedChild ? child : GetParent(child, true);

            while (parent != null)
            {
                foundElement = parent as T;

                if (foundElement != null)
                {
                    if (additionalCheckFunc == null)
                    {
                        return foundElement;
                    }
                    else
                    {
                        if (additionalCheckFunc(foundElement))
                            return foundElement;
                    }
                }

                parent = GetParent(parent, true);
            }

            return null;
        }



        /// <summary>
        /// 如果指定的元素是可视化树中某个父元素的子元素，则返回true。此方法适用于Visual、FrameworkElement和FrameworkContentElement。
        /// </summary>
        /// <param name="element">The element that is potentially a child of the specified parent.</param>
        /// <param name="parent">The element that is potentially a parent of the specified element.</param>
        /// <returns>The <see cref="bool"/></returns>
        //public static bool IsDescendantOf(this DependencyObject element, DependencyObject parent)
        //{
        //    return IsDescendantOf(element, parent, true);
        //}

        /// <summary>
        /// 如果指定的元素是可视化树中某个父元素的子元素，则返回true。此方法适用于Visual、FrameworkElement和FrameworkContentElement。
        /// </summary>
        /// <param name="element">The element that is potentially a child of the specified parent.</param>
        /// <param name="parent">The element that is potentially a parent of the specified element.</param>
        /// <param name="recurseIntoPopup">The recurseIntoPopup<see cref="bool"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsDescendantOf(this DependencyObject element, DependencyObject parent, bool recurseIntoPopup)
        {
            while (element != null)
            {
                if (element == parent)
                    return true;

                element = GetParent(element, recurseIntoPopup);
            }

            return false;
        }



        /// <summary>
        /// 尝试在可视树中定位给定项，从给定位置的依赖关系对象开始。
        /// </summary>
        public static T FindVisualByPoint<T>(this UIElement reference, Point point)
            where T : DependencyObject
        {
            if (!(reference.InputHitTest(point) is DependencyObject element)) 
                return null;
            else if (element is T t) 
                return t;
            else 
                return FindVisualParent<T>(element);
        }


        /// <summary>
        /// 分析可视树或逻辑树，获取所有元素。
        /// </summary>
        public static IEnumerable<DependencyObject> GetChilds(this DependencyObject parent, bool forceUsingVisualTreeHelper = false)
        {
            if (parent == null) yield break;

            if (!forceUsingVisualTreeHelper && (parent is ContentElement || parent is FrameworkElement))
            {
                foreach (var obj in LogicalTreeHelper.GetChildren(parent))
                {
                    if (obj is DependencyObject @object)
                        yield return @object;
                }
            }
            else
            {
                var count = VisualTreeHelper.GetChildrenCount(parent);

                for (var i = 0; i < count; i++)
                    yield return VisualTreeHelper.GetChild(parent, i);
            }
        }

        /// <summary>
        /// 这种方法是WPF的一种替代方法 <see cref="VisualTreeHelper.GetParent" /> 方法，该方法支持内容元素。
        /// 注意，对于内容元素此方法返回元素的逻辑树！
        /// </summary>
        public static DependencyObject GetVisualParent(this DependencyObject child)
        {
            if (child == null) return null;

            //handle content elements separately
            if (child is ContentElement contentElement)
            {
                var parent = ContentOperations.GetParent(contentElement);

                if (parent != null) return parent;

                return contentElement is FrameworkContentElement fce ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            else if (child is FrameworkElement frameworkElement)
            {
                var parent = frameworkElement.Parent;

                if (parent != null) return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }

        /// <summary>
        /// 尽量返回指定元素的父元素
        /// 它将试图按这个顺序找到视觉parent、逻辑parent、逻辑模板parent。
        /// 它只适用于Visual、FrameworkElement或FrameworkContentElement。
        /// </summary>
        public static DependencyObject GetParent(this DependencyObject child, bool recurseIntoPopup)
        {
            if (recurseIntoPopup)
            {
                if (child is Popup popup && popup.PlacementTarget != null)
                    return popup.PlacementTarget;
            }

            var parent = !(child is Visual visual) ? null : VisualTreeHelper.GetParent(visual);

            if (parent == null)
            {
                if (child is FrameworkElement fe)
                {
                    parent = fe.Parent;
                    parent ??= fe.TemplatedParent;
                }
                else if (child is FrameworkContentElement fce)
                {
                    parent = fce.Parent;
                    parent ??= fce.TemplatedParent;
                }
            }

            return parent;
        }



        public static IEnumerable<DependencyObject> VisualDepthFirstTraversal(this DependencyObject node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            yield return node;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++)
            {
                var child = VisualTreeHelper.GetChild(node, i);
                foreach (var descendant in child.VisualDepthFirstTraversal())
                {
                    yield return descendant;
                }
            }
        }

        public static IEnumerable<DependencyObject> VisualBreadthFirstTraversal(this DependencyObject node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++)
            {
                var child = VisualTreeHelper.GetChild(node, i);
                yield return child;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++)
            {
                var child = VisualTreeHelper.GetChild(node, i);

                foreach (var descendant in child.VisualDepthFirstTraversal())
                {
                    yield return descendant;
                }
            }
        }

        public static bool IsAncestorOf(this DependencyObject parent, DependencyObject node)
        {
            return node != null && parent.VisualDepthFirstTraversal().Contains(node);
        }

        /// <summary>
        /// Returns full visual ancestry, starting at the leaf.
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public static IEnumerable<DependencyObject> GetVisualAncestry(this DependencyObject leaf)
        {
            while (leaf != null)
            {
                yield return leaf;
                leaf = VisualTreeHelper.GetParent(leaf);
            }
        }

        public static IEnumerable<DependencyObject> GetLogicalAncestry(this DependencyObject leaf)
        {
            while (leaf != null)
            {
                yield return leaf;
                leaf = LogicalTreeHelper.GetParent(leaf);
            }
        }

        public static bool IsDescendantOf(this DependencyObject leaf, DependencyObject ancestor)
        {
            DependencyObject parent = null;
            foreach (var node in leaf.GetVisualAncestry())
            {
                if (Equals(node, ancestor))
                    return true;

                parent = node;
            }

            return parent.GetLogicalAncestry().Contains(ancestor);
        }
    }
}