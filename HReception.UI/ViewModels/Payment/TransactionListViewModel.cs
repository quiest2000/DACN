using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Infrastructure.Bases;
using HReception.Logic.Services.Interfaces.Payment;
using Xamarin.Forms;

namespace HReception.UI.ViewModels.Payment
{
    public class TransactionListViewModel : ViewModelBase
    {
        private readonly IPaymentService _paymentService;
        public TransactionListViewModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        #region overrides

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            FromDate = DateTime.Today;
            ToDate = DateTime.Now;
            await OnSearchCommandExecute();
            base.ViewIsAppearing(sender, e);
        }

        #endregion

        #region Properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SearchId { get; set; }
        public string PatientCode { get; set; }
        public List<TransactionReponse> Transactions { get; set; }
        public TransactionReponse SelectedTransaction { get; set; }
        #endregion

        #region Commands

        #region ViewDetailCommand
        private ICommand _viewDetailCommand;
        /// <summary>
        /// Gets the ViewDetailCommand command.
        /// </summary>
        public ICommand ViewDetailCommand => _viewDetailCommand ?? (_viewDetailCommand = new Command(async () => { await ViewDetailCommandExecute(); }, CanExecuteViewDetailCommand));

        private bool CanExecuteViewDetailCommand()
        {
            return SelectedTransaction != null;
        }

        /// <summary>
        /// Method to invoke when the command ViewDetailCommand is executed.
        /// </summary>
        private async Task ViewDetailCommandExecute()
        {
            //var vm = ServiceLocator.Default.ResolveType<TransactionDetailViewModel>();
            //vm.Transaction = SelectedTransaction;
            //vm.Details = await _paymentService.GetDetails(SelectedTransaction.Id);
            //await _uiVisualizerService.ShowAsync(vm);
        }
        #endregion


        #region BackCommand
        private ICommand _backCommand;
        /// <summary>
        /// Gets the BackCommand command.
        /// </summary>
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(async () => { await OnBackCommandExecute(); }));

        /// <summary>
        /// Method to invoke when the BackCommand command is executed.
        /// </summary>
        private async Task OnBackCommandExecute()
        {
            //_uiCompositionService.Activate<HomeViewModel>(DefinedRegions.MainContent);
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
            //_pleaseWaitService.Show();
            //int.TryParse(SearchId, out var id);
            //var trans = await _paymentService.GetTransactions(FromDate, ToDate, id, PatientCode ?? string.Empty);
            //Transactions = trans.ToList();
            //SelectedTransaction = Transactions.FirstOrDefault();
            //_pleaseWaitService.Hide();
        }
        #endregion

        #endregion
    }
}