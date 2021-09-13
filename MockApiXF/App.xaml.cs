namespace MockApiXF
{
    using Xamarin.Forms;

    public partial class App : Application
    {
        public static INavigation Nav { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
