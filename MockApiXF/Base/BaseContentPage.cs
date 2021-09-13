namespace MockApiXF.Base
{
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

    public class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
    {
        protected double width;
        protected double height;
        protected T _viewModel;

        public T ViewModel
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = new T
                    {
                        Page = this
                    };
                }
                return _viewModel;
            }
        }

        public BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            (BindingContext as BaseViewModel).OnAppearingAsync();
        }

        protected override void OnDisappearing()
        {
            (BindingContext as BaseViewModel).OnDisappearingAsync();
        }
    }
}
