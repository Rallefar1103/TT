using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;

using Android.OS;
using Android.Content;

namespace TurfTankRegistrationApplication.Droid
{
    
    [Activity(Label = "TurfTankRegistrationApplication", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    /// <summary>
    /// This intentFilter is set to catch a Redirect from the Oauth2 server, It has to match the redirect send to the authenticator and the allowed callback on the OAuth server.
    /// This Redirect URI has to be verified in App.cs : protected override async void OnAppLinkRequestReceived(Uri uri)
    /// 
    /// Categories: CategoryBrowsable Defines that this activity can be invoked from a browser.
    /// DataScheme: The Application name , in this case borrowed from another TurfTank application.
    /// DataHost: The Domain of the OAuth server.
    /// DataPathPrefix: the name part of the callback specifying the path to the callback.
    /// 
    /// </summary>
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
                DataScheme = "dk.turftank.turftankregistrationapplication",//"cloud.turftank.osm",//
                DataHost = "dev-ggbq2i2p.us.auth0.com",//"auth.turftank.cloud",//
                DataPathPrefix = "/android/dk.turftank.turftankregistrationapplication/callback",//"//login - callback",//
                AutoVerify = true)]
    ///dk.turftank.turftankregistrationapplication://dev-ggbq2i2p.us.auth0.com/android/dk.turftank.turftankregistrationapplication/callback



    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState); //this is for loggin in with Turftank
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

        /// <summary>
        /// This function is called when an intent is triggered by the intent filter in  line 24
        /// This triggers the function OnAppLinkRequestReceived() in App.cs , it is only used when
        /// OAuth login is set to native.
        /// </summary>
        /// <param name="intent"></param>
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            //Auth0.OidcClient.ActivityMediator.Instance.Send(intent.DataString);//Jeg er lidt i tvivl om hved denne gør???
        }
    }
}