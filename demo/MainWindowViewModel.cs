using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.TextFormatting;
using System.Windows.Threading;
using Zhai.Famil.Common.Mvvm;
using Zhai.Famil.Controls;

namespace Zhai.Famil.Demo
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public IEnumerable<IconKind> IconKinds => Enum.GetValues<IconKind>();

        private IEnumerable<IconKind> icons = Enum.GetValues<IconKind>();
        public IEnumerable<IconKind> Icons
        {
            get => icons;
            set => Set(() => Icons, ref icons, value);
        }

        private String searchKind;
        public String SearchKind
        {
            get => searchKind;
            set => Set(() => SearchKind, ref searchKind, value);
        }

        public async void SearchIcon()
        {
            string iconKindString = SearchKind;

            if (string.IsNullOrWhiteSpace(iconKindString))
            {
                Icons = IconKinds;
            }
            else
            {
                Icons = await Task.Run(() => IconKinds
                    .Where(x => x.ToString().IndexOf(iconKindString, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    .ToList());
            }
        }



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

        public IEnumerable<ItemData> Items => TextLines.Select((t, i) => new ItemData { IsSelected = false, Index = i + 1, Name = t.Split(" ").First(), Description = t, Number = (new Random()).Next(10, 100) });

        public IEnumerable<ItemData> Items3 => Items.Take(3);

        public ObservableCollection<ItemData> EditItems => new ObservableCollection<ItemData>(Items);

        public IEnumerable<int> Indexes => Items.Select((x, i) => i + 1);


        private String hintText;
        public String HintText
        {
            get => hintText;
            set => Set(() => HintText, ref hintText, value);
        }

        private IEnumerable<String> listLines;
        public IEnumerable<String> ListLines
        {
            get => listLines;
            set => Set(() => ListLines, ref listLines, value);
        }

        public MainWindowViewModel()
        {
            ListLines = TextLines;
        }

        private string validationStringValue;
        public string ValidationStringValue
        {
            get => validationStringValue;
            set => Set(() => ValidationStringValue, ref validationStringValue, value);
        }

        private bool validationboolValue;
        public bool ValidationBoolValue
        {
            get => validationboolValue;
            set => Set(() => ValidationBoolValue, ref validationboolValue, value);
        }

        private bool isShowAnimate;
        public bool IsShowAnimate
        {
            get => isShowAnimate;
            set => Set(() => IsShowAnimate, ref isShowAnimate, value);
        }
    }

    internal class ItemData : ViewModelBase
    {
        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => Set(() => IsSelected, ref isSelected, value);
        }

        private int index;
        public int Index
        {
            get => index;
            set => Set(() => Index, ref index, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => Set(() => Name, ref name, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => Set(() => Description, ref description, value);
        }

        private int number;
        public int Number
        {
            get => number;
            set => Set(() => Number, ref number, value);
        }
    }
}
