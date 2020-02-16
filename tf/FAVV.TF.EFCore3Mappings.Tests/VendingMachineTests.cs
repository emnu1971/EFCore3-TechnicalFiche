using FAVV.TF.EFCore3Mappings.Domain;
using FluentAssertions;
using System;
using Xunit;
using static FAVV.TF.EFCore3Mappings.Domain.Money;

namespace FAVV.TF.EFCore3Mappings.Tests
{

    public class VendingMachineTests
    {
        [Fact]
        public void Return_money_empties_money_in_transaction()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.InsertMoney(OneEuroCoin);

            vendingMachine.ReturnMoney();

            vendingMachine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void Inserted_money_goes_to_money_in_transaction()
        {
            var vendingMachine = new VendingMachine();

            vendingMachine.InsertMoney(FiftyEuroCent);
            vendingMachine.InsertMoney(TwentyEuroCent);
            vendingMachine.InsertMoney(FiveEuroCent);

            vendingMachine.MoneyInTransaction.Amount.Should().Be(0.75m);
        }

        [Fact]
        public void Cannot_insert_more_than_one_cent_or_coin_at_a_time()
        {
            var vendingMachine = new VendingMachine();
            var tenCent = FiveEuroCent + FiveEuroCent;

            Action action = () => vendingMachine.InsertMoney(tenCent);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Money_in_transaction_goes_to_money_inside_after_purchase()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.InsertMoney(OneEuroCoin);
            vendingMachine.InsertMoney(OneEuroCoin);

            vendingMachine.Vend();

            vendingMachine.MoneyInTransaction.Should().Be(None);
            vendingMachine.MoneyInside.Amount.Should().Be(2m);
        }

    }
}
