﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.FamilyContorls
{
    public class Slider : System.Windows.Controls.Slider
    {
        static Slider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Slider), new FrameworkPropertyMetadata(typeof(Slider)));
        }
    }
}
