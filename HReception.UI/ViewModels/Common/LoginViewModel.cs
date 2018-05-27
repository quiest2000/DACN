using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Infrastructure.Bases;
using HReception.Logic.Services.Interfaces.Common;
using HReception.Logic.Utils.Extensions;
using Xamarin.Forms;

namespace HReception.UI.ViewModels.Common
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ISecurityService _securityService;

        public LoginViewModel(ISecurityService securityService)
        {
            _securityService = securityService;
#if DEBUG
            UserName = "nv1";
            Password = "123456";
#endif
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        #region LoginCommand
        private ICommand _loginCommand;
        /// <summary>
        /// Gets the LoginCommand command.
        /// </summary>
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(async() => { await LoginCommandExecute(); }, CanExecuteLoginCommand));

        private bool CanExecuteLoginCommand()
        {
            return !UserName.IsNullOrEmpty() && !Password.IsNullOrEmpty();
        }

        /// <summary>
        /// Method to invoke when the command LoginCommand is executed.
        /// </summary>
        private async Task LoginCommandExecute()
        {
            //_pleaseWaitService.Show();
            //var rs = await _securityService.Login(UserName, Password);
            //if (!rs.IsValid)
            //{
            //    await _messageService.ShowWarningAsync("Tên đăng nhập hoặc mật khẩu không đúng.");
            //    _pleaseWaitService.Hide();
            //    return;
            //}
            //UserSigningMessage.SendWith(rs);
            //_pleaseWaitService.Hide();
        }
        #endregion

    }
}