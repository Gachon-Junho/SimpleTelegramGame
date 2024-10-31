using System;

namespace SimpleTelegramGame.Networking.Encryption
{
    public class BasicEncryptor : IEncryptor
    {
        public delegate int PlayerIdToTokenIndex(long playerId);

        private readonly PlayerIdToTokenIndex playerIdToTokenIndex;
        private readonly long[] bigPrimes;

        public BasicEncryptor(long[] bigPrimes, PlayerIdToTokenIndex playerIdToTokenIndex)
        {
            this.bigPrimes = bigPrimes;
            this.playerIdToTokenIndex = playerIdToTokenIndex;
        }

        public BasicEncryptor(long[] bigPrimes) 
            : this(bigPrimes, BasicPlayerIdToTokenIndex)
        {
        }

        private static int SumDigits(long value)
        {
            if (value < 10) return (int)value;

            long sum = 0;

            while (value != 0)
            {
                long rem;

                value = Math.DivRem(value, 10, out rem);
                sum += rem;
            }

            if (sum >= 10)
                sum = SumDigits(sum);

            return (int)sum;
        }

        public static int BasicPlayerIdToTokenIndex(long playerId) => SumDigits(playerId) - 1;

        public long Obfuscate(long playerId, long score)
        {
            int index = playerIdToTokenIndex(playerId);
            long token = bigPrimes[index];

            return score * token;
        }
    }
}