using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Services.Interfaces.Payment;
using Xamarin.Forms;
using HReception.Logic.Utils.Extensions;
using HReception.UI.PageModels.Common;
using HReception.Logic.Services.Interfaces.Patients;

namespace HReception.UI.PageModels.Payment
{
    public class TransactionListPageModel : PageModelBase
    {
        private bool _initSearch = false;
        private readonly IPaymentService _paymentService;
        public TransactionListPageModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        #region overrides
        public override async void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData is null)
                return;
            
            if (returnedData is bool dataChanged)
            {
                SearchCommand.Execute(null);
                return;
            }

            if (returnedData is PatientDto selectedPatient)
            {
                //go to assigmnent page
                await CoreMethods.PushPageModel<AssignmentPageModel>(selectedPatient);
                return;
            }
        }
        public override void Init(object initData)
        {
            CurrentPage.Title = "DS giao dịch";
            From = DateTime.Now;
            To = DateTime.Now;
            base.Init(initData);
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            if (!_initSearch)
                SearchCommand.Execute(null);
            _initSearch = true;
            base.ViewIsAppearing(sender, e);
        }
        #endregion

        #region Properties

        //public DateTime SelectedDate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string KeyWord { get; set; }
        public IList<TransactionReponse> Transactions { get; set; }
        #endregion

        #region Commands

        #region ViewDetailCommand

        private ICommand _ViewDetailCommand;

        public ICommand ViewDetailCommand => _ViewDetailCommand ?? (_ViewDetailCommand = new Command<TransactionReponse>(async (arg) => { await ViewDetailCommandExecute(arg); }));

        private async Task ViewDetailCommandExecute(TransactionReponse arg)
        {
            if (arg == null)
                return;

            var details = await _paymentService.GetDetails(arg.Id);
            arg.Details = details;
            await CoreMethods.PushPageModel<TransactionDetailPageModel>(arg);
        }

        #endregion

        #region SearchCommand
        private ICommand _searchCommand;
        /// <summary>
        /// Gets the SearchCommand command.
        /// </summary>
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command(async () => { await OnSearchCommandExecute(); }));

        /// <summary>
        /// Method to invoke when the SearchCommand command is executed.
        /// </summary>
        private async Task OnSearchCommandExecute()
        {
            try
            {
                IsBusy = true;
                Transactions = await _paymentService.GetTransactions(From.Date, To.Date.AddDays(1), 0, KeyWord.IsNullOrEmpty() ? string.Empty : KeyWord.Trim());
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region GoToAssignmentPageCommand

        private ICommand _GoToAssignmentPageCommand;

        public ICommand GoToAssignmentPageCommand => _GoToAssignmentPageCommand ?? (_GoToAssignmentPageCommand = new Command(async () => { await GoToAssignmentPageCommandExecute(); }));

        private async Task GoToAssignmentPageCommandExecute()
        {
            await CoreMethods.PushPageModel<HomePageModel>(true, true);
        }

        #endregion

        #endregion
    }
}
