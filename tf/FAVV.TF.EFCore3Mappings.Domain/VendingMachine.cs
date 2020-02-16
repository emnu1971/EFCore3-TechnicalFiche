using System;
using System.Linq;
using static FAVV.TF.EFCore3Mappings.Domain.Money;

namespace FAVV.TF.EFCore3Mappings.Domain
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 01-2020
    /// Purpose     : Vendingmachine
    /// </summary>
    public class VendingMachine
    {
        public virtual Money MoneyInside { get; set; } = None;
        public virtual Money MoneyInTransaction { get; set; } = None;
        public VendingMachine() { }
        public virtual void InsertMoney(Money money)
        {
            Money[] euroCentsAndCoins =
            {
                FiveEuroCent,TenEuroCent,TwentyEuroCent,FiftyEuroCent,OneEuroCoin,TwoEuroCoin
            };

            if (!euroCentsAndCoins.Contains(money))
            {
                throw new InvalidOperationException();
            }

            MoneyInTransaction += money;
        }
        public virtual void ReturnMoney()
        {
            MoneyInTransaction = None;
        }
        public virtual void Vend()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }
        public virtual void InitializeMoneyInside(Money money)
        {
            MoneyInside = money;
        }
    }
}
