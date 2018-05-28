using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Services.Interfaces.Payment;
using Xamarin.Forms;
using FreshMvvm;

namespace HReception.UI.PageModels.Payment
{
    public class TransactionListPageModel : PageModelBase
    {
        private readonly IPaymentService _paymentService;
        public TransactionListPageModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        #region overrides

        public override void Init(object initData)
        {
            FromDate = DateTime.Today;
            ToDate = DateTime.Now;
            SearchCommand.Execute(null);
            base.Init(initData);
        }

        #endregion

        #region Properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SearchId { get; set; }
        public string PatientCode { get; set; }
        public List<TransactionReponse> Transactions { get; set; }
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
                int.TryParse(SearchId, out var id);
                var trans = await _paymentService.GetTransactions(FromDate, ToDate, id, PatientCode ?? string.Empty);
                Transactions = trans.ToList();
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #endregion
    }
}