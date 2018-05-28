using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HReception.UI.Infrastructure;
using FreshMvvm;
using HReception.Logic.Constants;
using HReception.UI.PageModels.Common;
using HReception.UI.Utils;
using HReception.UI.Utils.Extensions;
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
            NavigationExtensions.GoToLoginPage();
        }
    }
}
