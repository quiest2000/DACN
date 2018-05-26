using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.UI.Infrastructure.Context;
using HReception.Core;
using HReception.Core.Context.EfModels;
using HReception.Core.Context.Enum;
using HReception.Logic.Services.Interfaces.Payment;
using HReception.Logic.Utils.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HReception.Logic.Services.Implementations.Payment
{
    public class PaymentService : IPaymentService
    {
        public NewTransactionReponse CreateTransaction(NewTransactionRequest request)
        {
            var now = DateTime.Now;
            using (var context = SimulatorContext.CreateContext())
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    var transaction = new Transaction
                    {
                        PatientCode = request.PatientCode,
                        Amount = request.Amount,
                        Date = now,
                        Encrypt = now.Ticks.ToString(),
                        Note = request.Note,
                        ReferenceCode = now.Ticks.ToString(),
                        Status = TransactionStatus.WaitingForPayment
                    };
                    transaction.Details.AddRange(request.ListItems.Select(aa => new TransactionDetail
                    {
                        Transaction = transaction,
                        Note = aa.Note,
                        Amount = aa.Amount,
                        ItemCode = aa.ItemCode,
                        Total = aa.Total,
                        UnitName = aa.UnitName,
                        UnitPrice = aa.UnitPrice,
                    }));
                    context.Transactions.Add(transaction);
                    context.SaveChanges();
                    scope.Complete();
                }
            }

            return new NewTransactionReponse { Result = NewTransactionResult.Succeeded };
        }

        public async Task<IList<TransactionReponse>> GetTransactions(DateTime fromDate, DateTime toDate, int transactionId, string patientCode)
        {
            fromDate = fromDate <= DateTime.MinValue ? DateTime.Today : fromDate;
            toDate = toDate <= DateTime.MinValue ? DateTime.Today.AddDays(1) : toDate;
            using (var context = SimulatorContext.CreateContext())
            {
                var results = await context.Transactions.Include(aa => aa.Patient).Include(aa => aa.Details)
                    .Where(aa => aa.Date >= fromDate && aa.Date < toDate
                                                     && (transactionId <= 0 || aa.Id == transactionId)
                                                     && (patientCode == "" || aa.PatientCode == patientCode))
                    .Select(aa => new TransactionReponse
                    {
                        Id = aa.Id,
                        PatientCode = aa.PatientCode,
                        PatientName = aa.Patient.FullName,
                        Amount = aa.Amount,
                        Date = aa.Date,
                        Encrypt = aa.Encrypt,
                        Note = aa.Note,
                        ReferenceCode = aa.ReferenceCode,
                        StatusId = aa.StatusId
                    }).ToListAsync();
                results.ForEach(aa =>
                {
                    switch ((TransactionStatus)aa.StatusId)
                    {
                        case TransactionStatus.WaitingForPayment:
                            aa.StatusName = "Chờ thanh toán";
                            break;
                        case TransactionStatus.Paid:
                            aa.StatusName = "Đã thanh toán";
                            break;
                        case TransactionStatus.WaitingForRefund:
                            aa.StatusName = "Chờ hoàn trả";
                            break;
                        case TransactionStatus.Refunded:
                            aa.StatusName = "Đã hoàn trả";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                });
                return results;
            }
        }

        public async Task<IList<TransactionDetailDto>> GetDetails(int transactionId)
        {
            using (var context = SimulatorContext.CreateContext())
            {
                var details = await context.TransactionDetails.AsNoTracking().Include(aa => aa.Item).Where(aa => aa.TransactionId == transactionId).Select(aa =>
                        new TransactionDetailDto
                        {
                            TransactionId = aa.TransactionId,
                            Id = aa.Id,
                            Amount = aa.Amount,
                            ItemCode = aa.ItemCode,
                            ItemName = aa.Item.ItemName,
                            Note = aa.Note,
                            Total = aa.Total,
                            UnitName = aa.UnitName,
                            UnitPrice = aa.UnitPrice
                        }).ToListAsync();
                return details;
            }
        }
        public IList<ItemReponse> GetAllItems()
        {
            var rs = new List<ItemReponse>();
            using (var context = SimulatorContext.CreateContext())
            {
                rs = context.Items.Select(aa => new ItemReponse
                {
                    ItemCode = aa.ItemCode,
                    ItemName = aa.ItemName,
                    UnitName = aa.UnitName,
                    UnitPrice = aa.UnitPrice,
                    SearchField = aa.ItemCode.ToLower()
                }).ToList();
            }
            return rs;
        }
    }
}