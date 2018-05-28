using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using HReception.Logic.Constants;
using HReception.Logic.Services.Interfaces.Common;
using HReception.Logic.Utils.Extensions;
using HReception.UI.PageModels.Payment;
using Xamarin.Forms;
using HReception.UI.Utils.Extensions;

namespace HReception.UI.PageModels.Common
{
    public class LoginPageModel : PageModelBase
    {
        private readonly ISecurityService _securityService;

        public LoginPageModel(ISecurityService securityService)
        {
            _securityService = securityService;
#if DEBUG
            UserName = "nv1";
            Password = "123456";
#endif
        }

        public override void Init(object initData)
        {
            CurrentPage.Title="Đăng nhập";
            base.Init(initData);
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        #region LoginCommand
        private ICommand _loginCommand;
        /// <summary>
        /// Gets the LoginCommand command.
        /// </summary>
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(async () => { await LoginCommandExecute(); }, CanExecuteLoginCommand));

        private bool CanExecuteLoginCommand()
        {
            return !UserName.IsNullOrEmpty() && !Password.IsNullOrEmpty() && !IsBusy;
        }

        /// <summary>
        /// Method to invoke when the command LoginCommand is executed.
        /// </summary>
        private async Task LoginCommandExecute()
        {
            IsBusy = true;
            var rs = await _securityService.Login(UserName, Password);
            IsBusy = false;
            if (!rs.IsValid)
            {
                await CoreMethods.DisplayAlert("Warning", "Tên đăng nhập hoặc mật khẩu không đúng.", "Ok");
                return;
            }
            this.GoToMainPage();
        }
        #endregion

    }
}