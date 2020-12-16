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
            ChangeBaseSimcard = new Command(() => NavigateToScanPage("Base"));
            ChangeBaseSN = new Command(async () => await GetBaseSerialNumber());
        }

        public void NavigateToScanPage(string component)
        {
            ScanPage scanPage = new ScanPage();
            scanPage.vm.Title = "Scanning " + component;
            scanPage.QRMustContain = "Type:" + component;
            Navigation.PushAsync(scanPage);
        }

        public async Task GetBaseSerialNumber()
        {
            try
            {
                GPS response = await GPS.API.GetById("basestation|68e1e7e3-270f-4235-89bb-2f315bd7b8a5");
                Console.WriteLine(response);

            } catch (HttpRequestException e)
            {
                Console.WriteLine("CATCH: " + e);
            }
            
            
        }
    }
}
