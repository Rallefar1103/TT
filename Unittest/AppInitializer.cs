using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Unittest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.companyname.turftankregistrationapplication").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}