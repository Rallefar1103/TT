using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class ListWifiViewModel : INotifyPropertyChanged
    {
        public bool IsLoading
        {
            get { return IsLoading;  }
            set
            {
                IsLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public Command PressButton { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ListWifiViewModel()
        {
            //MessagingCenter.Subscribe<RoverRegistrationViewModel, bool>(this, "wifiScanResult", (sender, data) => {


            //});
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
