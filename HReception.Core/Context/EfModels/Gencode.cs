using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Client.UI.Infrastructure.Context;
using Client.UI.Infrastructure.Context.Enum;
using HReception.Core.Context.Enum;

namespace HReception.Core.Context.EfModels
{
    public class Gencode : IEntityBase
    {
        public Gencode()
        {
            LastPick = DateTime.Now;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EntityTypeFullName { get; set; }
        [MaxLength(32)]
        public string Prefix { get; set; }
        /// <summary>
        /// số thứ tự (mã) được tạo gan nhat
        /// </summary>
        public int LastGeneratedNumber { get; private set; }
        public ResetUnit Reset { get; set; }
        public DateTime LastPick { get; private set; }
        public string Next()
        {
            var now = DateTime.Now;
            switch (Reset)
            {
                case ResetUnit.None:
                    break;
                case ResetUnit.Yearly:
                    if (now.Year != LastPick.Year)
                        LastGeneratedNumber = 0;
                    break;
                case ResetUnit.Monthly:
                    if (now.Month != LastPick.Month || now.Year != LastPick.Year)
                        LastGeneratedNumber = 0;
                    break;
                case ResetUnit.Dayly:
                    if (now.Day != LastPick.Day || now.Month != LastPick.Month || now.Year != LastPick.Year)
                        LastGeneratedNumber = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            LastPick = now;
            LastGeneratedNumber++;
            return $"{Prefix}-{now:yyyyMMdd}.{LastGeneratedNumber}";
        }
    }
}