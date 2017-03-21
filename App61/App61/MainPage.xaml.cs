using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App61
{
    public class Car
    {
        public string Name { get; set; }
        public string monery { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var items = new List<Car>();
            items.Add(new Car { Name = "Xamarin.com" });
            items.Add(new Car { Name = "Twitter" });
            items.Add(new Car { Name = "Facebook" });
            items.Add(new Car { Name = "Internet ad" });
            items.Add(new Car { Name = "Online article" });
            items.Add(new Car { Name = "Magazine ad" });
            items.Add(new Car { Name = "Friends" });
            items.Add(new Car { Name = "At work" });
            var multiPage = new MultipleSelectBase<Car>(items) { };
            Content = multiPage;
        }
    }
}