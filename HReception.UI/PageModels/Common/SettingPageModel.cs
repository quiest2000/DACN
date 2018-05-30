using System;
using System.Windows.Input;
using Xamarin.Forms;
using HReception.UI.Utils.Extensions;
using System.Threading.Tasks;
using FreshMvvm;
using HReception.Logic.Services.Interfaces.Common;

namespace HReception.UI.PageModels.Common
{
    public class SettingPageModel : PageModelBase
    {
        private readonly ISecurityService _securityService;
        public SettingPageModel(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Init(object initData)
        {
            CurrentPage.Title = "Settings";
            CurrentUser = _securityService.CurrentUser();
            base.Init(initData);
        }

        #region LogoutCommand

        private ICommand _logoutCommand;

        public ICommand LogoutCommand => _logoutCommand ?? (_logoutCommand = new Command(async () => { await LogoutCommandExecute(); }));

        public LoginResultDto CurrentUser { get; private set; }

        private async Task LogoutCommandExecute()
        {
            var mrs = await this.ShowConfirmAsync("Bạn có chắc muốn đăng xuất?");
            if (!mrs)
                return;
            _securityService.Logout();
            NavigationExtensions.GoToLoginPage();
        }

        #endregion
    }
}
