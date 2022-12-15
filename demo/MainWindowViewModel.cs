using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.TextFormatting;
using System.Windows.Threading;
using Zhai.FamilTheme;

namespace Zhai.FamilThemeDemo
{
    internal class MainWindowViewModel : BaseViewModel
    {
        public IEnumerable<IconKind> IconKinds => Enum.GetValues<IconKind>();

        public IEnumerable<String> TextLines => new String[] {
            "Egestas adipiscing est.",
            "Nulla wisi varius, tincidunt etiam.",
            "Sociis sit, ut imperdiet.",
            "Cras curabitur vivamus.",
            "Lorem ipsum dolor sit amet, maecenas imperdiet, nullam lacus.",
            "Pretium wisi wisi, morbi tellus nulla.",
            "Consectetuer quam, at in.",
            "Fringilla vestibulum lacinia, morbi vitae sapien, eget porta.",
            "Sed iaculis, accumsan tortor.",
            "Amet mi, ullamcorper erat.",
            "Tristique netus nunc, donec in quis, aptent aliquet pellentesque.",
            "Nunc justo vitae, pellentesque felis duis, cursus mi purus.",
            "Commodo pharetra cum, lorem sodales, augue a facilisis.",
            "Aliquam facilisis mi, etiam penatibus blanditiis, mus vel morbi.",
            "Nisl nulla, interdum nulla aenean.",
            "Sapien ligula, dui lacinia, donec ante.",
            "Aenean etiam libero.",
            "Velit laudantium, penatibus pellentesque, sed quisque pulvinar.",
            "Quis repellendus.",
            "Enim augue volutpat, nec cubilia, non turpis."
        };

        public IEnumerable<String> TextLines3 => TextLines.Take(3);

        public IEnumerable<String> TextLines10 => TextLines.Take(10);

        public IEnumerable<ItemData> Items => TextLines.Select((t, i) => new ItemData { Index = i + 1, Name = t.Split(" ").First(), Description = t });

        public IEnumerable<ItemData> Items3 => Items.Take(3);

        private String hintText;
        public String HintText
        {
            get => hintText;
            set => SetProperty(ref hintText, value);
        }

        private IEnumerable<String> listLines;
        public IEnumerable<String> ListLines
        {
            get => listLines;
            set => SetProperty(ref listLines, value);
        }

        public MainWindowViewModel()
        {
            ListLines = TextLines;
        }
    }

    internal class ItemData
    { 
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
