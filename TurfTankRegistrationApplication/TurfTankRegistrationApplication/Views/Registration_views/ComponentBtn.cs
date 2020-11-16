using System;

using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Views.Registration_views
{
    public class ComponentBtn : Button
    {
        public ComponentBtn(string ComponentType, Command RegistrateComponent)
        {
            Text = ComponentType;
            Command = RegistrateComponent;
            FontAttributes = FontAttributes.Bold;
            BackgroundColor = Color.White;
            TextColor = Color.Black;
            BorderColor = Color.Green;
            BorderWidth = 2;
            Margin = 10;
            CornerRadius = 25;
            HeightRequest = 70;
            WidthRequest = 275;
            HorizontalOptions = LayoutOptions.Center;
        }
    }

}

