using System;
using System.Net.Http;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Pages;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class BaseRegistrationViewModel
    {
        public INavigation Navigation { get; set; }

        public Command ChangeBaseSimcard { get; }
        public Command ChangeBaseSN { get; }

        public BaseRegistrationViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            ChangeBaseSimcard = new Command(() => NavigateToScanPage("basestation"));
            ChangeBaseSN = new Command(async () => await GetBaseSerialNumber());
        }

        public void NavigateToScanPage(string component)
        {
            ScanPage scanPage = new ScanPage();
            scanPage.vm.Title = "Scanning " + component;
            scanPage.QRMustContain = component;
            Navigation.PushAsync(scanPage);
        }

        // Should use bluetooth TODO
        public async Task GetBaseSerialNumber()
        {
            try
            {
                GPS response = await GPS.API.GetById("basestation|97cce3bc-cd81-4c74-9dfa-8b048baedbe5");
                Console.WriteLine("BASE ID: " + response.ID + "BASE SERIAL NUMBER: " + response.SerialNumber);

                if (response.SerialNumber != null)
                {
                    MessagingCenter.Send(this, "BaseSerialNumber", response.SerialNumber);
                    await Application.Current.MainPage.DisplayAlert("Success!", "Got Base Serial Number: " + response.SerialNumber, "OK");
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
