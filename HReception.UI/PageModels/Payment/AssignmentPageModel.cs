using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Services.Interfaces.Patients;
using HReception.Logic.Services.Interfaces.Payment;
using Xamarin.Forms;

namespace HReception.UI.PageModels.Payment
{
    public class AssignmentPageModel : PageModelBase
    {
        private readonly IPaymentService _paymentService;
        public AssignmentPageModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public override void Init(object initData)
        {
            CurrentPage.Title = "Giao dịch mới";
            base.Init(initData);
        }
        #region Properties
        public PatientDto Patient { get; set; }
        public ObservableCollection<ItemReponse> SelectedItems { get; set; }
        public int Count => SelectedItems?.Count ?? 0;
        public double Total => SelectedItems?.Sum(aa => aa.Total) ?? 0;
        #endregion

        #region Commands

        #region SelectItemCommand
        private ICommand _selectItemCommand;
        /// <summary>
        /// Gets the SelectItemCommand command.
        /// </summary>
        public ICommand SelectItemCommand => _selectItemCommand ?? (_selectItemCommand = new Command(async () => { await OnSelectItemCommandExecute(); }));

        /// <summary>
        /// Method to invoke when the SelectItemCommand command is executed.
        /// </summary>
        private async Task OnSelectItemCommandExecute()
        {
            //var newItems = new ObservableCollection<ItemReponse>();
            //await _uiVisualizerService.ShowDialogAsync<SelectItemViewModel>(null, (sender, args) =>
            //   {
            //       if (!args.Result.HasValue || !args.Result.Value)
            //           return;
            //       var view = args.DataContext as SelectItemViewModel;
            //       if (view != null)
            //           newItems = new ObservableCollection<ItemReponse>(view.Items.Where(aa => aa.IsChecked));
            //   });
            //if (!newItems.Any())
            //    return;
            //var curItems = new ObservableCollection<ItemReponse>(SelectedItems ?? new ObservableCollection<ItemReponse>());
            //newItems.ForEach(newItem =>
            //{
            //    var curItem = curItems.FirstOrDefault(aa => aa.ItemCode == newItem.ItemCode);
            //    if (curItem != null)
            //    {
            //        curItem.Qty++;
            //        curItem.Total = curItem.Qty * curItem.UnitPrice;
            //    }
            //    else
            //    {
            //        newItem.Qty = 1;
            //        newItem.Total = newItem.Qty * newItem.UnitPrice;
            //        curItems.Add(newItem);
            //    }
            //});
            //SelectedItems = new ObservableCollection<ItemReponse>(curItems);
        }
        #endregion

        #region SaveCommand
        private ICommand _saveCommand;
        /// <summary>
        /// Gets the SaveCommand command.
        /// </summary>
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => { await OnSaveCommandExecute(); }, CanExecuteSaveCommand));

        private bool CanExecuteSaveCommand()
        {
            return SelectedItems?.Any() ?? false;
        }

        /// <summary>
        /// Method to invoke when the SaveCommand command is executed.
        /// </summary>
        private async Task OnSaveCommandExecute()
        {
            //var mrs = await _messageService.ShowAsync("Bạn có chắc muốn lưu phiếu đăng ký?", "xác nhận", MessageButton.OKCancel,
            //    MessageImage.Question);
            //if (mrs != MessageResult.OK)
            //    return;

            //var reponse = _paymentService.CreateTransaction(new NewTransactionRequest
            //{
            //    PatientCode = Patient.PatientCode,
            //    Amount = Total,
            //    ListItems = SelectedItems.Select(aa => new ItemDetailRequest
            //    {
            //        Amount = aa.Qty,
            //        Total = aa.Total,
            //        ItemCode = aa.ItemCode,
            //        ItemName = aa.ItemName,
            //        UnitPrice = aa.UnitPrice,
            //        UnitName = aa.UnitName,
            //    }).ToList()
            //});
            //if (reponse.HasErrorOnRemoting)
            //    return;

            //if (reponse.Result == NewTransactionResult.Succeeded)
            //{
            //    //await _messageService.ShowInformationAsync("Đã lưu phiếu đăng ký!");
            //    await OnCancelCommandExecute();
            //}
            //else
            //{
            //    await _messageService.ShowErrorAsync("Lưu phiếu thất bại, vui lòng thử lại sau.");
            //}
        }
        #endregion

        #region CancelCommand
        private ICommand _cancelCommand;
        /// <summary>
        /// Gets the CancelCommand command.
        /// </summary>
        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(async () => { await OnCancelCommandExecute(); }));

        /// <summary>
        /// Method to invoke when the CancelCommand command is executed.
        /// </summary>
        private async Task OnCancelCommandExecute()
        {
            //_uiCompositionService.Activate<HomeViewModel>(DefinedRegions.MainContent);
        }
        #endregion


        #region RemoveItemCommand
        private ICommand _removeItemCommand;
        /// <summary>
        /// Gets the RemoveItemCommand command.
        /// </summary>
        public ICommand RemoveItemCommand => _removeItemCommand ?? (_removeItemCommand = new Command<ItemReponse>(async (obj) => { await RemoveItemCommandExecute(obj); }));

        /// <summary>
        /// Method to invoke when the command RemoveItemCommand is executed.
        /// </summary>
        private async Task RemoveItemCommandExecute(ItemReponse arg)
        {
            //var mrs = await _messageService.ShowAsync($"Bạn có chắc muốn xóa dịch vụ [{arg.ItemName}]?", "Xác nhận",
            //    MessageButton.YesNo, MessageImage.Question);
            //if (mrs != MessageResult.Yes)
            //    return;
            //SelectedItems = new ObservableCollection<ItemReponse>(SelectedItems.Where(aa => aa != arg).ToArray());
        }
        #endregion

        #endregion
    }
}