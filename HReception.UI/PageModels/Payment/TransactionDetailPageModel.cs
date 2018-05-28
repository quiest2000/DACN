using System.Collections.Generic;
using HReception.Logic.Services.Interfaces.Payment;

namespace HReception.UI.PageModels.Payment
{
    public class TransactionDetailPageModel : PageModelBase
    {
        public override void Init(object initData)
        {
            Transaction = initData as TransactionReponse;
            Details = Transaction?.Details ?? new List<TransactionDetailDto>();
            base.Init(initData);
        }
        public TransactionReponse Transaction { get; set; }
        public IList<TransactionDetailDto> Details { get; set; }
    }
}