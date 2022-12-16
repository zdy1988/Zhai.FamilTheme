using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.FamilTheme
{
    public class ValidationExtension
    {
        public static readonly DependencyProperty IsErrorMessageVisibleOnFocusProperty = DependencyProperty.RegisterAttached("IsErrorMessageVisibleOnFocus", typeof(bool), typeof(ValidationExtension), new PropertyMetadata(true));
        public static bool GetIsErrorMessageVisibleOnFocus(DependencyObject obj) => (bool)obj.GetValue(IsErrorMessageVisibleOnFocusProperty);
        public static void SetIsErrorMessageVisibleOnFocus(DependencyObject obj, bool value) => obj.SetValue(IsErrorMessageVisibleOnFocusProperty, value);


        public static readonly DependencyProperty IsErrorMessageVisibleOnMouseOverProperty = DependencyProperty.RegisterAttached("IsErrorMessageVisibleOnMouseOver", typeof(bool), typeof(ValidationExtension), new PropertyMetadata(true));
        public static bool GetIsErrorMessageVisibleOnMouseOver(DependencyObject obj) => (bool)obj.GetValue(IsErrorMessageVisibleOnMouseOverProperty);
        public static void SetIsErrorMessageVisibleOnMouseOver(DependencyObject obj, bool value) => obj.SetValue(IsErrorMessageVisibleOnMouseOverProperty, value);


        public static readonly DependencyProperty ErrorMessagePlacementProperty = DependencyProperty.RegisterAttached("ErrorMessagePlacement", typeof(ValidationErrorIndicatorPlacement), typeof(ValidationExtension), new PropertyMetadata(ValidationErrorIndicatorPlacement.Top));
        public static ValidationErrorIndicatorPlacement GetErrorMessagePlacement(DependencyObject obj) => (ValidationErrorIndicatorPlacement)obj.GetValue(ErrorMessagePlacementProperty);
        public static void SetErrorMessagePlacement(DependencyObject obj, ValidationErrorIndicatorPlacement value) => obj.SetValue(ErrorMessagePlacementProperty, value);
    }
}
