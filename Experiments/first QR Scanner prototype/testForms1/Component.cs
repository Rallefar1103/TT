using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace testForms1
{
    public class Component: INotifyPropertyChanged
    {
        public Component()
        {
        }

        private int _id;
        private string  _serialnumber;
        private string  _componentType;
        private Component _isMountedIn;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public int id { get; set; }

        public string Serialnumber
        {
            get { return _serialnumber; }
            set {
                    _serialnumber = value;
                    OnPropertyChanged();
                }
        }

        public string ComponentType
        {
            get { return _componentType; }
            set {
                    _componentType = value;
                    OnPropertyChanged();
                }
        }

        public Component IsMountedIn
        {
            get { return _isMountedIn; }
            set {
                    _isMountedIn = value;
                    OnPropertyChanged();
                }
        }


        
    }
}
