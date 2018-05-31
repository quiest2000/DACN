using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Services.Interfaces.Patients;
using HReception.Logic.Services.Interfaces.Payment;
using Xamarin.Forms;
using HReception.Logic.Utils.Extensions;
using HReception.UI.Utils.Extensions;

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
            Patient = initData as PatientDto;
            base.Init(initData);
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (!(returnedData is ItemReponse[] selectedItems) || selectedItems.IsNullOrEmpty())
                return;

            var curItems = new ObservableCollection<ItemReponse>(SelectedItems ?? new ObservableCollection<ItemReponse>());

            foreach (var newItem in selectedItems)
            {
                var curItem = curItems.FirstOrDefault(aa => aa.ItemCode == newItem.ItemCode);
                if (curItem != null)
                {
                    curItem.Qty++;
                    curItem.Total = curItem.Qty * curItem.UnitPrice;
                }
                else
                {
                    newItem.Qty = 1;
                    newItem.Total = newItem.Qty * newItem.UnitPrice;
                    curItems.Add(newItem);
                }
            }

            SelectedItems = new ObservableCollection<ItemReponse>(curItems);
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
            await CoreMethods.PushPageModel<SelectItemPageModel>(null, true);
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
            return true;
        }

        /// <summary>
        /// Method to invoke when the SaveCommand command is executed.
        /// </summary>
        private async Task OnSaveCommandExecute()
        {
            if (SelectedItems.IsNullOrEmpty())
                return;
            var reponse = _paymentService.CreateTransaction(new NewTransactionRequest
            {
                PatientCode = Patient.PatientCode,
                Amount = Total,
                ListItems = SelectedItems.Select(aa => new ItemDetailRequest
                {
                    Amount = aa.Qty,
                    Total = aa.Total,
                    ItemCode = aa.ItemCode,
                    ItemName = aa.ItemName,
                    UnitPrice = aa.UnitPrice,
                    UnitName = aa.UnitName,
                }).ToList()
            });

            if (reponse.Result != NewTransactionResult.Succeeded)
                await this.ShowWarningAsync("Không thể lưu phiếu, vui lòng thử lại sau.");
            await CoreMethods.PopPageModel(data: reponse.Result == NewTransactionResult.Succeeded);
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
            if (arg is null)
                return;

            var mrs = await this.ShowConfirmAsync("Bạn có chắc muốn xoá dịch vụ?");
            if (!mrs)
                return;
            SelectedItems = new ObservableCollection<ItemReponse>(SelectedItems.Where(aa => aa != arg).ToArray());
        }
        #endregion

        #endregion
    }
}