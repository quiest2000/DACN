using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HReception.UI.Infrastructure;
using FreshMvvm;
using HReception.Logic.Constants;
using HReception.UI.PageModels.Common;
using HReception.Logic.Mapping;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HReception.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Bootstrap.Register();
            MappingConfig.Config();
            var nextPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            MainPage = new FreshNavigationContainer(nextPage, AppStack.LoginStack);
        }
       
    }
}
