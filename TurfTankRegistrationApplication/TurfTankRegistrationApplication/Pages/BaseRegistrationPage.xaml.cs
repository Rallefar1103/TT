using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class BaseRegistrationPage : ContentPage
    {
        public BaseRegistrationPage()
        {
            BaseRegistrationViewModel BaseVM = new BaseRegistrationViewModel(Navigation);
            BindingContext = BaseVM;

            Title = "Register Base Station";

            Button BaseSN = new Button
            {
                HeightRequest = 60,
                WidthRequest = 150,
                CornerRadius = 25,
                Text = "Base SN",
                BackgroundColor = Color.White,
            };

            Button BaseSimcard = new Button
            {
                HeightRequest = 60,
                WidthRequest = 150,
                CornerRadius = 25,
                Text = "Base Simcard",
                BackgroundColor = Color.White,
            };

            Label RegisterBase = new Label
            {
                Text = "Register Base",
                TextColor = Color.White,
                FontSize = 35,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,

            };

            BaseSN.SetBinding(Button.CommandProperty, "ChangeBaseSN");
            BaseSimcard.SetBinding(Button.CommandProperty, "ChangeBaseSimcard");

            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Padding = 20,

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = GridLength.Auto },

                },

                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = 100 },
                    new RowDefinition { Height = GridLength.Auto },
                },

                
            };

            Grid.SetRow(RegisterBase, 0);
            Grid.SetColumnSpan(RegisterBase, 2);
            Grid.SetColumn(BaseSN, 0);
            Grid.SetRow(BaseSN, 2);

            Grid.SetColumn(BaseSimcard, 1);
            Grid.SetRow(BaseSimcard, 2);

            grid.Children.Add(RegisterBase);
            grid.Children.Add(BaseSN);
            grid.Children.Add(BaseSimcard);

            Content = grid;


            InitializeComponent();
        }
    }
}
