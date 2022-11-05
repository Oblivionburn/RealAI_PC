using System;
using System.Security.Cryptography;

namespace Real_AI.Util
{
    public class CryptoRandom : RandomNumberGenerator
    {
        #region Variables

        private static RandomNumberGenerator r;

        #endregion

        #region Constructor

        public CryptoRandom()
        {
            r = Create();
        }

        #endregion

        #region Methods

        public override void GetBytes(byte[] buffer)
        {
            r.GetBytes(buffer);
        }

        public double NextDouble()
        {
            byte[] b = new byte[4];
            r.GetBytes(b);
            return (double)BitConverter.ToUInt32(b, 0) / uint.MaxValue;
        }

        public int Next(int minValue, int maxValue)
        {
            return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
        }

        public int Next()
        {
            return Next(0, int.MaxValue);
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            r.GetNonZeroBytes(data);
        }

        #endregion
    }
}