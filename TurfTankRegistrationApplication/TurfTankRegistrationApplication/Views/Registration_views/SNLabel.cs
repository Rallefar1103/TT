using System;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Views.Registration_views
{
    public class SNLabel : Label
    {
        public SNLabel(string ComponentSN)
        {
            Text = ComponentSN;
            TextColor = Color.White;
            HorizontalOptions = LayoutOptions.Center;
        }
    }
}

