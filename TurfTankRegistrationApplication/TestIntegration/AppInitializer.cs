using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TestIntegration
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp                                                     // The start command for configuring the app for testing.
                    .Android                                                            // Specifying it is Android we work on
                    .InstalledApp("com.companyname.turftankregistrationapplication")    // The name of the app (ie. TurfTankRegistrationApplication). Can be found in Properties
                    .EnableLocalScreenshots()                                           // Makes it possible to take and store screenshots during testing. (So far saves weird places)
                                                                                        //.DeviceSerial("SerialForASpecificEmulatorIfWanted");              // Option for connecting a specific device                                                
                    .StartApp();                                                        // Command for starting the app
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}