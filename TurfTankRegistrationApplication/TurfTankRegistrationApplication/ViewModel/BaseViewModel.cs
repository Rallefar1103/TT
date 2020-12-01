using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using TurfTankRegistrationApplication.Model;
//using TurfTankRegistrationApplication.Services;

namespace TurfTankRegistrationApplication.ViewModel
{
    /// <summary>
    /// Contains the base methods that all ViewModels use.
    /// SetProperty(ref variableName, value) is the most important!
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        // SUGGESTION Remove this from BaseViewModel and set it in the ViewModels that use it
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        // SUGGESTION Remove this from BaseViewModel and set it in the ViewModels that use it
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        #region INotifyPropertyChanged
        /// <summary>
        ///     "Multicast event for property change notifications." - https://www.danrigby.com/2015/09/12/inotifypropertychanged-the-net-4-6-way/
        /// </summary>
        /// <helper>The previous sentence means "A general purpose handler for registering when events change some property, so that it can be updated instantly in the View"</helper>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// This is used to set a property and update the View with the new value.
        /// It is basically just a very fancy "set", which also updates the view.
        /// </summary>
        /// <typeparam name="T">A generic type, which is set on the instantiation of the method</typeparam>
        /// <param name="variableName">A reference to the variable which should hold the value</param>
        /// <param name="setterValue">The 'value' which is assigned in the setter property</param>
        /// <param name="propertyName">The name of the property. Read docs in OnPropertyChanged</param>
        /// <param name="onChanged">Not completely sure...</param>
        /// <returns>Returns true if there is a new value to the storage, otherwise returns false</returns>
        protected bool SetProperty<T>(ref T variableName, T setterValue,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(variableName, setterValue))
                return false;

            variableName = setterValue;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// The method uses the PropertyChanged event handler to put the new value out into the View.
        /// </summary>
        /// <param name="propertyName">The name of the variable being assigned a new value</param>
        /// <helper>The "?" after PropertyChanged means "aside from its usual type, this one can also be NULL". This is done to make it thread-safe: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and- </helper>
        /// <helper>"Invoke" is a function which means "Find everywhere in "this" View where this propertyName is used and update it to the new value"</helper>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
