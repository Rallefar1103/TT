using System;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
//using TurfTankRegistrationApplication.Resources;

namespace TurfTankRegistrationApplication.Pages
{
    public class FrontPage : ContentPage
    {
        Image image = new Image { Source = "RobotPic.png" };
        public FrontPage()
        {
            Content = new StackLayout
            {
                Padding = new Thickness(20,2,2,20),
                Children =
                {
                    new Label { Text = "TurfTank"},
                    image
                    .Center().FillHorizontal(),
                    new Label { Text = "TurfTank igen"}
                    .Center()
                }
            };
        }
    }
}

