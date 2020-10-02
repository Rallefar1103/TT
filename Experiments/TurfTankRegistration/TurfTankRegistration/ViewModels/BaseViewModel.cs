using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TurfTankRegistration.ViewModels
{
    /// <summary>
    ///     Implementation of INotifyPropertyChanged so all ViewModels have access to the functionality.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
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
        /// <param name="storage">A reference to the variable which should hold the value</param>
        /// <param name="value">The value which is given to the storage</param>
        /// <param name="propertyName">The name of the property. Read docs in OnPropertyChanged</param>
        /// <returns>Returns true if there is a new value to the storage, otherwise returns false</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        /// <summary>
        /// The method uses the PropertyChanged event handler to put the new value out into the View.
        /// </summary>
        /// <param name="propertyName">The name of the variable being assigned a new value</param>
        /// <helper>The "?" after PropertyChanged means "aside from its usual type, this one can also be NULL". This is done to make it thread-safe: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and- </helper>
        /// <helper>"Invoke" is a function which means "Find everywhere in "this" View where this propertyName is used and update it to the new value"</helper>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
