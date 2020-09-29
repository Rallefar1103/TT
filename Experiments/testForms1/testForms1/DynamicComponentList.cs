using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace testForms1
{
    public class DynamicComponentList: INotifyPropertyChanged
    {
        public DynamicComponentList()
        {
        }
        private ObservableCollection<Component> _componentList;
        private Component _mySelectedComponent;


        public ObservableCollection<Component> ComponentList
        {
            get { return _componentList; }
            set {
                    _componentList = value;
                    OnPropertyChanged();
                }
        }

        public Component MySelectedComponent
        {
            get { return _mySelectedComponent; }
            set
                {
                    MySelectedComponent = value;
                    OnPropertyChanged();
                }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
