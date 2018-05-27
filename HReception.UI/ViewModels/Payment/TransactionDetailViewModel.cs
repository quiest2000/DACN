using System.Collections.Generic;
using HReception.Logic.Infrastructure.Bases;
using HReception.Logic.Services.Interfaces.Payment;

namespace HReception.UI.ViewModels.Payment
{
    public class TransactionDetailViewModel : ViewModelBase
    {
        public TransactionReponse Transaction { get; set; }
        public IList<TransactionDetailDto> Details { get; set; }
    }
}