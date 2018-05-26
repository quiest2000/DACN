using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HReception.UI.Infrastructure;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HReception.UI
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Bootstrap.Register();

            //todo: load main page
            MainPage = new ContentPage()
            {
                Content = new Label() { Text = "Hello HReception", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, BackgroundColor=Color.Blue}
            };
        }      
    }
}
