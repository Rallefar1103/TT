using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Xamarin.Auth;

namespace TurfTankRegistrationApplication.Droid
{
    
    [Activity(Label = "TurfTankRegistrationApplication", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
                DataScheme = "cloud.turftank.osm",//"dk.turftank.turftankregistrationapplication",
                DataHost = "auth.turftank.cloud",//"dev-ggbq2i2p.us.auth0.com",
                DataPathPrefix = "//login - callback",//"/android/dk.turftank.turftankregistrationapplication/callback",
                AutoVerify = true)]
    //dk.turftank.turftankregistrationapplication://dev-ggbq2i2p.us.auth0.com/android/dk.turftank.turftankregistrationapplication/callback

    

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState); //this is for loggin in with facebook
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

  protected override async void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            Auth0.OidcClient.ActivityMediator.Instance.Send(intent.DataString);//Jeg er lidt i tvivl om hved denne gør???
        }
    }
}