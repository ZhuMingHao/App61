using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static Xamarin.Forms.Device;

namespace App61
{
    public class MultipleSelectBase<T> : ContentView
    {
        public List<WrappedSelection<T>> WrappedItems = new List<WrappedSelection<T>>();

        public MultipleSelectBase(List<T> items)
        {
            WrappedItems = items.Select(item => new WrappedSelection<T>() { Item = item, IsSelected = false }).ToList();

            ListView mainList = new ListView()
            {
                ItemsSource = WrappedItems,
                ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
            };

            mainList.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                var o = (WrappedSelection<T>)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null; //de-select
            };
            Content = mainList;
        }
    }

    public class WrappedSelection<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public T Item { get; set; }
        private bool isSelected = false;

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
    }

    public class WrappedItemSelectionTemplate : ViewCell
    {
        public WrappedItemSelectionTemplate() : base()
        {
            Label name = new Label();
            name.SetBinding(Label.TextProperty, new Binding("Item.Name"));
            Switch mainSwitch = new Switch();
            mainSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));
            RelativeLayout layout = new RelativeLayout();
            layout.Children.Add(name,
                Constraint.Constant(5),
                Constraint.Constant(5),
                Constraint.RelativeToParent(p => p.Width - 60)

            );
            layout.Children.Add(mainSwitch,
                Constraint.RelativeToParent(p => p.Width - 55),
                Constraint.Constant(5),
                Constraint.Constant(50)

            );
            View = layout;
        }
    }

    //public class MultiSelectBasePage<T> : ContentPage
    //{
    //    private ListView _lstView;
    //    private IEnumerable<WrappedSelection<T>> _wrappedItems;

    //    public IEnumerable<T> Items
    //    {
    //        get { return _wrappedItems?.Select(i => i.Item) ?? new T[0]; }

    //        set
    //        {
    //            _wrappedItems = value?.Select(item => new WrappedSelection<T> { Item = item });
    //            // _lstView.ItemsSource = _wrappedItems;
    //        }
    //    }

    //    public MultiSelectListView(string bindingProperty, IEnumerable<T> items = null)
    //    {
    //        Items = items;

    //        if (string.IsNullOrWhiteSpace(bindingProperty))
    //            throw new ArgumentNullException(nameof(bindingProperty));

    //        WrappedItemSelectionTemplate.BindingProperty = bindingProperty;

    //        Content = _lstView = new ListView()
    //        {
    //            ItemsSource = _wrappedItems,
    //            ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate))
    //        };

    //        _lstView.ItemSelected += (sndr, e) =>
    //        {
    //            if (e.SelectedItem == null)
    //                return;

    //            var wrappedSelection = e.SelectedItem as WrappedSelection<T>;
    //            if (wrappedSelection != null)
    //                wrappedSelection.IsSelected = !wrappedSelection.IsSelected;

    //            var lstView = sndr as ListView;
    //            if (lstView != null)
    //                lstView.SelectedItem = null;
    //        };
    //    }
    //}

    //internal class WrappedItemSelectionTemplate : ViewCell
    //{
    //    public static string BindingProperty { get; internal set; }

    //    public WrappedItemSelectionTemplate()
    //    {
    //        var name = new Label { LineBreakMode = LineBreakMode.WordWrap, Style = Styles.TitleStyle };
    //        name.SetBinding(Label.TextProperty, new Binding($"Item{(!string.IsNullOrEmpty(BindingProperty) ? $".{BindingProperty}" : "")}"));

    //        var grid = new Grid
    //        {
    //            Children = { name },
    //            ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) } }
    //        };

    //        View = grid;
    //    }
    //}

    //internal class WrappedSelection<T> : INotifyPropertyChanged
    //{
    //    private bool _isSelected;

    //    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    //    public bool IsSelected
    //    {
    //        get { return _isSelected; }
    //        set
    //        {
    //            if (_isSelected != value)
    //            {
    //                _isSelected = value;
    //                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsSelected)));
    //            }
    //        }
    //    }

    //    public T Item { get; set; }
    //}
}