using System;

namespace FAVV.TF.EFCore3Mappings.Domain
{
    /// <summary>
    /// Money Class
    /// </summary>
    public class Money : ValueObject<Money>
    {
        public static readonly Money None           = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money FiveEuroCent   = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenEuroCent    = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money TwentyEuroCent = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money FiftyEuroCent  = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money OneEuroCoin    = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwoEuroCoin    = new Money(0, 0, 0, 0, 0, 1);
        public int FiveEuroCentCount { get; private set; }
        public int TenEuroCentCount { get; private set; }
        public int TwentyEuroCentCount { get; private set; }
        public int FiftyEuroCentCount { get; private set; }
        public int OneEuroCoinCount { get; private set; }
        public int TwoEuroCoinCount { get; private set; }

        public Money() { }
        public Money(
            int fiveEuroCentCount, 
            int tenEuroCentCount, 
            int twentyEuroCentCount, 
            int fiftyEuroCentCount,
            int oneEuroCoinCount,
            int twoEuroCoinCount) : this()
        {
            if (fiveEuroCentCount < 0) throw new InvalidOperationException();
            if (tenEuroCentCount < 0) throw new InvalidOperationException();
            if (twentyEuroCentCount < 0) throw new InvalidOperationException();
            if (fiftyEuroCentCount < 0) throw new InvalidOperationException();
            if (oneEuroCoinCount < 0) throw new InvalidOperationException();
            if (twoEuroCoinCount < 0) throw new InvalidOperationException();
            FiveEuroCentCount = fiveEuroCentCount;
            TenEuroCentCount = tenEuroCentCount;
            TwentyEuroCentCount = twentyEuroCentCount;
            FiftyEuroCentCount = fiftyEuroCentCount;
            OneEuroCoinCount = oneEuroCoinCount;
            TwoEuroCoinCount = twoEuroCoinCount;
        }
        public decimal Amount => FiveEuroCentCount * 0.05m + TenEuroCentCount * 0.10m + TwentyEuroCentCount * 0.20m + FiftyEuroCentCount * 0.50m + OneEuroCoinCount + TwoEuroCoinCount * 2;
        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new Money(
                money1.FiveEuroCentCount + money2.FiveEuroCentCount,
                money1.TenEuroCentCount + money2.TenEuroCentCount,
                money1.TwentyEuroCentCount + money2.TwentyEuroCentCount,
                money1.FiftyEuroCentCount + money2.FiftyEuroCentCount,
                money1.OneEuroCoinCount + money2.OneEuroCoinCount,
                money1.TwoEuroCoinCount + money2.TwoEuroCoinCount
                );
            return sum;
        }
        public static Money operator -(Money money1, Money money2)
        {
            Money sum = new Money(
                money1.FiveEuroCentCount - money2.FiveEuroCentCount,
                money1.TenEuroCentCount - money2.TenEuroCentCount,
                money1.TwentyEuroCentCount - money2.TwentyEuroCentCount,
                money1.FiftyEuroCentCount - money2.FiftyEuroCentCount,
                money1.OneEuroCoinCount - money2.OneEuroCoinCount,
                money1.TwoEuroCoinCount - money2.TwoEuroCoinCount
                );
            return sum;
        }
        public override string ToString() { return $"€{Amount}"; }
        #region protected interface

        protected override bool EqualsCore(Money other)
        {
            return
                FiveEuroCentCount == other.FiveEuroCentCount &&
                TenEuroCentCount == other.TenEuroCentCount &&
                TwentyEuroCentCount == other.TwentyEuroCentCount &&
                FiftyEuroCentCount == other.FiftyEuroCentCount  &&
                OneEuroCoinCount == other.OneEuroCoinCount &&
                TwoEuroCoinCount == other.TwoEuroCoinCount;
        }
        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = FiveEuroCentCount;

                hashCode = (hashCode * 397) ^ FiveEuroCentCount;
                hashCode = (hashCode * 397) ^ TenEuroCentCount;
                hashCode = (hashCode * 397) ^ TwentyEuroCentCount;
                hashCode = (hashCode * 397) ^ FiftyEuroCentCount;
                hashCode = (hashCode * 397) ^ OneEuroCoinCount;
                hashCode = (hashCode * 397) ^ TwoEuroCoinCount;

                return hashCode;
            }
        }
        #endregion protected interface
    }
}
