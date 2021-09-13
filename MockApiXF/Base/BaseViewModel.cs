namespace MockApiXF.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class BaseViewModel : ObservableObject
    {
        private INavigation FormsNavigation { get => App.Nav; }

        ~BaseViewModel() { }

        public ContentPage Page { get; set; }

        public virtual Task OnAppearingAsync()
            => Task.CompletedTask;

        public virtual Task OnDisappearingAsync()
            => Task.CompletedTask;

        protected Task PushAsync(Page page)
            => Device.InvokeOnMainThreadAsync(() => FormsNavigation.PushAsync(page, true));

        protected Task NavigateBack()
            => FormsNavigation.PopAsync();

        protected Task NavigateToModal(Page page, bool animation = false)
            => Device.InvokeOnMainThreadAsync(() => FormsNavigation.PushModalAsync(page, animation = false));

        protected Task NavigateBackModalAsync()
            => Device.InvokeOnMainThreadAsync(() => FormsNavigation.PopModalAsync(true));
    }

    public class ObservableObject : INotifyPropertyChanged
    {
        protected virtual bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action? onChanged = null,
            Func<T, T, bool>? validateValue = null)
        {
            //if value didn't change
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            //if value changed but didn't validate
            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}