using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HReception.Logic.Services.Interfaces.Payment
{
    public interface IPaymentService
    {
        NewTransactionReponse CreateTransaction(NewTransactionRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toDate"></param>
        /// <param name="searchId">Empty value = skip this condition</param>
        /// <param name="patientCode">Empty value = skip this condition</param>
        /// <param name="fromDate"></param>
        /// <returns></returns>
        Task<IList<TransactionReponse>> GetTransactions(DateTime fromDate, DateTime toDate, int searchId = 0,
            string patientCode = "");

        Task<IList<TransactionDetailDto>> GetDetails(int transactionId);
        IList<ItemReponse> GetAllItems();

    }
}