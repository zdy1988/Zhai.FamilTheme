using System;
using System.Linq;
using System.Reflection;

namespace Zhai.Famil.Common.Mvvm.Helpers
{
    /// <summary>
    /// Helper class for platform detection.
    /// </summary>
    internal static class DesignerLibrary
    {
        private static bool? _isInDesignMode;

        public static bool IsInDesignMode
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    var prop = System.ComponentModel.DesignerProperties.IsInDesignModeProperty;
                    _isInDesignMode
                        = (bool)System.ComponentModel.DependencyPropertyDescriptor
                                        .FromProperty(prop, typeof(System.Windows.FrameworkElement))
                                        .Metadata.DefaultValue;
                }

                return _isInDesignMode.Value;
            }
        }
    }
}
