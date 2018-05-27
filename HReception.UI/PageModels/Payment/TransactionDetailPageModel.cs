using System.Collections.Generic;
using HReception.Logic.Services.Interfaces.Payment;

namespace HReception.UI.PageModels.Payment
{
    public class TransactionDetailPageModel : PageModelBase
    {
        public TransactionReponse Transaction { get; set; }
        public IList<TransactionDetailDto> Details { get; set; }
    }
}