using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TurfTankRegistration.ViewModels;

namespace TurfTankRegistration.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentView
    {
        public StartPage()
        {
            InitializeComponent();
            BindingContext = new StartModel();
        }
    }
}