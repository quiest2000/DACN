using System;
using FreshMvvm;
using HReception.Logic.Constants;
using HReception.UI.PageModels.Common;
using HReception.UI.PageModels.Payment;
using Xamarin.Forms;

namespace HReception.UI.Utils.Extensions
{
    public static class NavigationExtensions
    {
        public static void GoToLoginPage()
        {
            var nextPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            Application.Current.MainPage = new FreshNavigationContainer(nextPage, AppStack.LoginStack);
        }

        public static void GoToMainPage(this FreshBasePageModel pageModel)
        {
            var nextTabbedPage = new FreshTabbedNavigationContainer(AppStack.MainAppStack);
            nextTabbedPage.AddTab<HomePageModel>("Home", "tab_feed.png");
            nextTabbedPage.AddTab<TransactionListPageModel>("Trans", "tab_about.png");
            nextTabbedPage.AddTab<SettingPageModel>("Settings", "tab_about.png");
            pageModel.CoreMethods.SwitchOutRootNavigation(AppStack.MainAppStack);
        }
    }
}
