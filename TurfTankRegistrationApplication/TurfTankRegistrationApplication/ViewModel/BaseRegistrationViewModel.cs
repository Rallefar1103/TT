using System;
using System.Net.Http;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Pages;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class BaseRegistrationViewModel: BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public GPS BaseStation { get; set; }

        public Action<object, string> BaseQRCallback;

        public Command ChangeBaseSimcard { get; }
        public Command ChangeBaseSN { get; }

        public Color BaseQRButtonColor { get; set; } = Color.White;

        public BaseRegistrationViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.BaseStation = new GPS();
            BaseQRCallback = new Action<object, string>(ScanCallback);
            BaseStation.ofType = GPSType.Base;
            ChangeBaseSimcard = new Command(() => NavigateToScanPage("basestation"));
            ChangeBaseSN = new Command(async () => await GetBaseSerialNumber());
            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", BaseQRCallback);
        }

        private void ScanCallback(object sender, string data)
        {
            if (data != null)
            {
                BaseQRButtonColor = Color.DarkGreen;
                OnPropertyChanged(nameof(BaseQRButtonColor));
            }
            MessagingCenter.Unsubscribe<ScanPage, string>(this, "Result");
        }

        public void NavigateToScanPage(string component)
        {
            ScanPage scanPage = new ScanPage(returntypeKey:"Result");
            scanPage.vm.Title = "Scanning " + component;
            scanPage.QRMustContain = component;
            Navigation.PushAsync(scanPage);
        }

        // Should use bluetooth TODO
        public async Task GetBaseSerialNumber()
        {
            try
            {
                BaseStation = await GPS.API.GetById("basestation|97cce3bc-cd81-4c74-9dfa-8b048baedbe5");
                Console.WriteLine("BASE ID: " + BaseStation.ID + "BASE SERIAL NUMBER: " + BaseStation.SerialNumber);

                if (BaseStation.SerialNumber != null)
                {
                    MessagingCenter.Send(this, "BaseSerialNumber", BaseStation.SerialNumber);
                    await Application.Current.MainPage.DisplayAlert("Success!", "Got Base Serial Number: " + BaseStation.SerialNumber, "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("OBS!", "Could not retrieve the serial number for Base", "OK");
                }
               

            } catch (HttpRequestException e)
            {
                Console.WriteLine("CATCH: " + e);
            }
            
            
        }
    }
}
