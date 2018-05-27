using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HReception.UI.Infrastructure;
using FreshMvvm;
using HReception.Logic.Constants;
using HReception.UI.PageModels.Common;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HReception.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Bootstrap.Register();
            var nextPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            MainPage = new FreshNavigationContainer(nextPage, AppStack.LoginStack);
        }
       
    }
}
