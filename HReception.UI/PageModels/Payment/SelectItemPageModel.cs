using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HReception.Logic.Services.Interfaces.Payment;
using HReception.Logic.Utils.Extensions;
using Xamarin.Forms;

namespace HReception.UI.PageModels.Payment
{
    public class SelectItemPageModel : PageModelBase
    {
        private readonly IPaymentService _paymentService;
        private IList<ItemReponse> _allitems;

        public SelectItemPageModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            _allitems = _paymentService.GetAllItems();
            Items = new ObservableCollection<ItemReponse>(_allitems);
            base.ViewIsAppearing(sender, e);
        }

        public string SearchCode { get; set; }

        public ObservableCollection<ItemReponse> Items { get; set; }


        #region SearchCommand
        private ICommand _searchCommand;

        /// <summary>
        /// Gets the SearchCommand command.
        /// </summary>
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command(SearchCommandExecute));
        /// <summary>
        /// Method to invoke when the command SearchCommand is executed.
        /// </summary>
        private void SearchCommandExecute()
        {
            var lower = (SearchCode ?? string.Empty).ToLower();
            Items = new ObservableCollection<ItemReponse>(_allitems.Where(aa =>
                lower.IsNullOrEmpty() || aa.SearchField != null && aa.SearchField.Contains(lower)));
        }
        #endregion

    }
}