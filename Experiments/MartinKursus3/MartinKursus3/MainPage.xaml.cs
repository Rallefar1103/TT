using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MartinKursus3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            List<Models.Car> cars = new List<Models.Car>()
            {
                new Models.Car(){ Make = "Skoda", Model = "Fabia", Price = 50000},
                new Models.Car(){ Make = "Skoda", Model = "Octavia", Price = 60000},
                new Models.Car(){ Make = "BMW", Model = "Nice", Price = 600000},
                new Models.Car(){ Make = "Ford", Model = "Ka", Price = 9000},
                new Models.Car(){ Make = "Ford", Model = "New Ka", Price = 16000}
            };
        }
    }
}
