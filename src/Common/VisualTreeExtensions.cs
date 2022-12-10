using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Zhai.FamilyContorls.Common
{
    /// <summary>
    /// 查找控件扩展方法
    /// </summary>
    public static class VisualTreeExtensions
    {
        #region Methods

        /// <summary>
        /// 在可视化树中查找给定名称的子项
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The <see cref="T"/></returns>
        public static T FindChild<T>(this DependencyObject parent, string childName)
            where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                var childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T) child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T) child;
                    break;
                }
            }

            return foundChild;
        }

        /// <summary>
        /// 这将搜索指定类型的子级。搜索是按层次进行的，广度优先（而不是深度优先）。
        /// </summary>
        /// <typeparam name="T">The type of the element to find</typeparam>
        /// <param name="parent">The root of the tree to search for. This element itself is not checked.</param>
        /// <param name="additionalCheck">The additionalCheck<see cref="Func{T, bool}"/></param>
        /// <returns>Returns the found element. Null if nothing is found.</returns>
        public static T FindChild<T>(this DependencyObject parent, Func<T, bool> additionalCheck = null)
                where T : DependencyObject
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            T child;

            for (var index = 0; index < childrenCount; index++)
            {
                child = VisualTreeHelper.GetChild(parent, index) as T;

                if (child != null)
                    if (additionalCheck == null)
                    {
                        return child;
                    }
                    else
                    {
                        if (additionalCheck(child))
                            return child;
                    }
            }

            for (var index = 0; index < childrenCount; index++)
            {
                child = FindChild(VisualTreeHelper.GetChild(parent, index), additionalCheck);

                if (child != null)
                    return child;
            }

            return null;
        }

        /// <summary>
        /// 根据控件名称，查找子控件
        ///     elementName为空时，查找指定类型的子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj<see cref="DependencyObject"/></param>
        /// <param name="elementName">The elementName<see cref="string"/></param>
        /// <returns>The <see cref="T"/></returns>
        public static T FindChildElement<T>(this DependencyObject obj, string elementName)
            where T : FrameworkElement
        {
            DependencyObject child = null;
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && ((T)child).Name == elementName || string.IsNullOrEmpty(elementName))
                    return (T)child;

                var grandChild = FindChildElement<T>(child, elementName);
                if (grandChild != null) return grandChild;
            }

            return null;
        }

        /// <summary>
        /// 根据控件名称，查找子控件集合。elementName为空时，查找指定类型的所有子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj<see cref="DependencyObject"/></param>
        /// <param name="elementName">The elementName<see cref="string"/></param>
        /// <returns>The <see cref="List{T}"/></returns>
        public static List<T> FindChildElements<T>(this DependencyObject obj, string elementName)
                where T : FrameworkElement
        {
            DependencyObject child = null;

            var childList = new List<T>();

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && ((T) child).Name == elementName || string.IsNullOrEmpty(elementName))
                {
                    childList.Add((T) child);
                }
                else
                {
                    var grandChildList = FindChildElements<T>(child, elementName);
                    if (grandChildList != null) childList.AddRange(grandChildList);
                }
            }

            return childList;
        }


        /// <summary>
        /// 搜索指定类型的父级
        /// </summary>
        /// <typeparam name="T">The type of the element to find</typeparam>
        /// <param name="startingObject">The node where the search begins. This element is not checked.</param>
        /// <returns>Returns the found element. Null if nothing is found.</returns>
        public static T FindParent<T>(this DependencyObject startingObject) 
            where T : DependencyObject
        {
            return FindParent<T>(startingObject, false, null);
        }

        /// <summary>
        /// 搜索指定类型的父级
        /// </summary>
        /// <typeparam name="T">The type of the element to find</typeparam>
        /// <param name="startingObject">The node where the search begins.</param>
        /// <param name="checkStartingObject">Should the specified startingObject be checked first.</param>
        /// <returns>Returns the found element. Null if nothing is found.</returns>
        public static T FindParent<T>(this DependencyObject startingObject, bool checkStartingObject) 
            where T : DependencyObject
        {
            return FindParent<T>(startingObject, checkStartingObject, null);
        }

        /// <summary>
        /// 搜索指定类型的父级
        /// </summary>
        /// <typeparam name="T">The type of the element to find</typeparam>
        /// <param name="startingObject">The node where the search begins.</param>
        /// <param name="checkStartingObject">Should the specified startingObject be checked first.</param>
        /// <param name="additionalCheck">The additionalCheck<see cref="Func{T, bool}"/></param>
        /// <returns>Returns the found element. Null if nothing is found.</returns>
        public static T FindParent<T>(this DependencyObject startingObject, bool checkStartingObject, Func<T, bool> additionalCheck) 
            where T : DependencyObject
        {
            T foundElement;
            var parent = checkStartingObject ? startingObject : GetParentObject(startingObject, true);

            while (parent != null)
            {
                foundElement = parent as T;

                if (foundElement != null)
                {
                    if (additionalCheck == null)
                    {
                        return foundElement;
                    }
                    else
                    {
                        if (additionalCheck(foundElement))
                            return foundElement;
                    }
                }

                parent = GetParentObject(parent, true);
            }

            return null;
        }

        /// <summary>
        /// 根据控件名称，查找父控件 elementName 为空时，查找指定类型的父控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj<see cref="DependencyObject"/></param>
        /// <param name="elementName">The elementName<see cref="string"/></param>
        /// <returns>The <see cref="T"/></returns>
        public static T FindParentElement<T>(this DependencyObject obj, string elementName)
                where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if (parent is T && (((T) parent).Name == elementName || string.IsNullOrEmpty(elementName)))
                    return (T) parent;
                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }


        /// <summary>
        /// The FindVisualChildItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The <see cref="DependencyObject" /></param>
        /// <returns>The <see cref="T" /></returns>
        public static T FindVisualChild<T>(this DependencyObject obj) 
            where T : DependencyObject
        {
            if (null != obj)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    var child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T) return (T)child;

                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
        }

        /// <summary>
        /// The FindVisualChildItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The <see cref="DependencyObject" /></param>
        /// <param name="name">The <see cref="string" /></param>
        /// <returns>The <see cref="T" /></returns>
        public static T FindVisualChild<T>(this DependencyObject obj, string name) where T : FrameworkElement
        {
            if (null != obj)
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    var child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T && (child as T).Name.Equals(name)) return (T) child;

                    var childOfChild = FindVisualChild<T>(child, name);
                    if (childOfChild != null && childOfChild is T && childOfChild.Name.Equals(name))
                        return childOfChild;
                }

            return null;
        }



        /// <summary>
        /// 分析可视树或逻辑树，获取所有元素。
        /// </summary>
        /// <param name="parent">The item to be processed.</param>
        /// <param name="forceUsingTheVisualTreeHelper">Sometimes it's better to search in the VisualTree (e.g. in tests)</param>
        /// <returns>The submitted item's child elements, if available.</returns>
        public static IEnumerable<DependencyObject> GetChildElements(this DependencyObject parent, bool forceUsingTheVisualTreeHelper = false)
        {
            if (parent == null) yield break;

            if (!forceUsingTheVisualTreeHelper && (parent is ContentElement || parent is FrameworkElement))
            {
                //use the logical tree for content / framework elements
                foreach (var obj in LogicalTreeHelper.GetChildren(parent))
                {
                    var depObj = obj as DependencyObject;
                    if (depObj != null) yield return (DependencyObject) obj;
                }
            }
            else
            {
                //use the visual tree per default
                var count = VisualTreeHelper.GetChildrenCount(parent);
                for (var i = 0; i < count; i++) yield return VisualTreeHelper.GetChild(parent, i);
            }
        }

        /// <summary>
        /// 分析可视树或逻辑树，以查找作为源项后代的给定类型的所有元素。
        /// </summary>
        /// <typeparam name="T">The type of the queried items.</typeparam>
        /// <param name="source">The source<see cref="DependencyObject"/></param>
        /// <param name="forceUsingTheVisualTreeHelper">Sometimes it's better to search in the VisualTree (e.g. in tests)</param>
        /// <returns>All descendants of <paramref name="source" /> that match the requested type.</returns>
        public static IEnumerable<T> GetChildElements<T>(this DependencyObject source, bool forceUsingTheVisualTreeHelper = false)
            where T : DependencyObject
        {
            if (source != null)
            {
                var childs = GetChildElements(source, forceUsingTheVisualTreeHelper);
                foreach (var child in childs)
                {
                    //analyze if children match the requested type
                    if (child != null && child is T) yield return (T)child;

                    //recurse tree
                    foreach (var descendant in GetChildElements<T>(child)) yield return descendant;
                }
            }
        }



        /// <summary>
        /// 如果指定的元素是可视化树中某个父元素的子元素，则返回true。此方法适用于Visual、FrameworkElement和FrameworkContentElement。
        /// </summary>
        /// <param name="element">The element that is potentially a child of the specified parent.</param>
        /// <param name="parent">The element that is potentially a parent of the specified element.</param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsDescendantOf(this DependencyObject element, DependencyObject parent)
        {
            return IsDescendantOf(element, parent, true);
        }

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

                element = GetParentObject(element, recurseIntoPopup);
            }

            return false;
        }



        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">The child<see cref="DependencyObject"/></param>
        /// <returns>The <see cref="T"/></returns>
        private static T FindParent2<T>(this DependencyObject child)
                where T : DependencyObject
        {
            //get parent item
            var parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            var parent = parentObject as T;

            return parent ?? FindParent2<T>(parentObject);
        }

        /// <summary>
        /// 尝试在可视树中定位给定项，从给定位置的依赖关系对象开始。
        /// </summary>
        /// <typeparam name="T">The type of the element to be found
        /// on the visual tree of the element at the given location.</typeparam>
        /// <param name="reference">The main element which is used to perform
        /// hit testing.</param>
        /// <param name="point">The position to be evaluated on the origin.</param>
        public static T FindElementByPoint<T>(this UIElement reference, Point point)
            where T : DependencyObject
        {
            DependencyObject element = reference.InputHitTest(point) as DependencyObject;

            if (element == null) return null;
            else if (element is T) return (T)element;
            else return FindParent2<T>(element);
        }



        /// <summary>
        /// 这种方法是WPF的一种替代方法 <see cref="VisualTreeHelper.GetParent" /> 方法，该方法支持内容元素。请记住，对于内容元素，此方法返回到元素的逻辑树！
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The <see cref="DependencyObject"/></returns>
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null) return null;

            //handle content elements separately
            var contentElement = child as ContentElement;
            if (contentElement != null)
            {
                var parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                var fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            var frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                var parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }

        /// <summary>
        /// 尽量返回指定元素的父元素。它将试图按这个顺序找到视觉parent、逻辑parent、逻辑模板parent。
        /// 它只适用于Visual、FrameworkElement或FrameworkContentElement。
        /// </summary>
        /// <param name="element">The <see cref="DependencyObject" /></param>
        /// <param name="recurseIntoPopup">The <see cref="bool" /></param>
        /// <returns>The <see cref="DependencyObject" /></returns>
        public static DependencyObject GetParentObject(DependencyObject element, bool recurseIntoPopup)
        {
            if (recurseIntoPopup)
            {
                // Case 126732 : To correctly detect parent of a popup we must do that exception case
                var popup = element as Popup;

                if (popup != null && popup.PlacementTarget != null)
                    return popup.PlacementTarget;
            }

            var visual = element as Visual;
            var parent = visual == null ? null : VisualTreeHelper.GetParent(visual);

            if (parent == null)
            {
                // No Visual parent. Check in the logical tree.
                var fe = element as FrameworkElement;

                if (fe != null)
                {
                    parent = fe.Parent;

                    if (parent == null) parent = fe.TemplatedParent;
                }
                else
                {
                    var fce = element as FrameworkContentElement;

                    if (fce != null)
                    {
                        parent = fce.Parent;

                        if (parent == null) parent = fce.TemplatedParent;
                    }
                }
            }

            return parent;
        }

        /// <summary>
        /// 尽量返回指定元素的父元素。它将试图按这个顺序找到视觉parent、逻辑parent、逻辑模板parent。
        /// 它只适用于Visual、FrameworkElement或FrameworkContentElement。
        /// </summary>
        /// <param name="element">The element<see cref="DependencyObject"/></param>
        /// <returns>The <see cref="DependencyObject"/></returns>
        public static DependencyObject GetParentObjectRecurseIntoPopup(this DependencyObject element)
        {
            return GetParentObject(element, true);
        }

        #endregion
    }
}