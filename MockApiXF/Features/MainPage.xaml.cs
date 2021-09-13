namespace MockApiXF
{
    using MockApiXF.Base;

    public partial class MainPage : MainPageViewXaml
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }

    public partial class MainPageViewXaml : BaseContentPage<MainViewModel> { }
}
