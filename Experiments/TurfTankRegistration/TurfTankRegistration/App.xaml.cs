﻿using System;
using TurfTankRegistration.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurfTankRegistration
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SigninPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
