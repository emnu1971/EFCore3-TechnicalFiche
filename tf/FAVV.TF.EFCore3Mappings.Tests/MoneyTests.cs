using FAVV.TF.EFCore3Mappings.Domain;
using FluentAssertions;
using System;
using Xunit;

namespace FAVV.TF.EFCore3Mappings.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void Sum_of_two_moneys_produces_correct_result()
        {
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money sum = money1 + money2;

            sum.FiveEuroCentCount.Should().Be(2);
            sum.TenEuroCentCount.Should().Be(4);
            sum.TwentyEuroCentCount.Should().Be(6);
            sum.FiftyEuroCentCount.Should().Be(8);
            sum.OneEuroCoinCount.Should().Be(10);
            sum.TwoEuroCoinCount.Should().Be(12);
        }

        [Fact]
        public void Subtraction_of_two_moneys_produces_correct_result()
        {
            Money money1 = new Money(2, 4, 6, 8, 10, 12);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money substraction = money1 - money2;

            substraction.FiveEuroCentCount.Should().Be(1);
            substraction.TenEuroCentCount.Should().Be(2);
            substraction.TwentyEuroCentCount.Should().Be(3);
            substraction.FiftyEuroCentCount.Should().Be(4);
            substraction.OneEuroCoinCount.Should().Be(5);
            substraction.TwoEuroCoinCount.Should().Be(6);
        }

        [Fact]
        public void Two_money_instances_are_equal_if_they_contain_the_same_money_amount()
        {
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void Two_money_instances_do_not_equal_if_they_contain_different_money_amounts()
        {
            Money oneEuro = new Money(0, 0, 0, 0, 1, 0);
            Money hundredEuroCent = new Money(0, 0, 0, 2, 0, 0);

            oneEuro.Should().NotBe(hundredEuroCent);
            oneEuro.GetHashCode().Should().NotBe(hundredEuroCent.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_value(
            int fiveEuroCentCount,
            int tenEuroCentCount,
            int twentyEuroCentCount,
            int fiftyEuroCentCount,
            int oneEuroCoinCount,
            int twoEuroCoinCount)
        {
            Action action = () => new Money(
                fiveEuroCentCount,
                tenEuroCentCount,
                twentyEuroCentCount,
                fiftyEuroCentCount,
                oneEuroCoinCount,
                twoEuroCoinCount);

            action.Should().Throw<InvalidOperationException>();
        }
    }
}
