using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class SplashPage : ContentPage
    {
        public string sendBackData { get; set; }
        public SplashPage()
        {
            InitializeComponent();
            sendBackData = "Received";
        }

        void SendMessageBack(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Hello", sendBackData);
        }

    }
}
