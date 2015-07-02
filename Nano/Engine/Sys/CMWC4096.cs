using System;
using System.Runtime.InteropServices;

namespace Nano.Engine.Sys
{
    /// <summary>
    /// Complimentarty multiply with carry pseudo random number generator.
    /// Adapted from public domain code by George Marsaglia
    /// see https://groups.google.com/d/msg/sci.crypt/MFxQfvvoW-Q/GbI7emUvFRIJ
    /// </summary>
    public class CMWC4096 : IRandom
    {
        private ulong[] Q = new ulong[4096];
        private ulong c = 362436;
        private ulong i = 4095;
        private const ulong MaxInt = 4294967296;

        public CMWC4096(int seed)
        {
            //seed the CMWC - Q array with random numbers from built in random class
            Random rnd = new Random(seed);
            for (int j = 0; j < 4096; j++)
            {
                Q[j] = (ulong)rnd.Next();
            }
        }

        /// <summary>
        /// Gets the next unsigned long value in the psuedorandom sequence
        /// </summary>
        /// <returns>A pseudorandom unsigned long value</returns>
        public ulong NextUnsignedLong()
        {
            ulong t, a = 18782L, b = 4294967295L;
            ulong r = b - 1;

            i = (i + 1) & 4095;
            t = a*Q[i] + c;
            c = (t >> 32);
            t = (t & b) + c;

            if (t > r)
            {
                c++;
                t = t - b;

            }
            return (Q[i] = r - t);
        }

        /// <summary>
        /// Gets the next int value in the psuedorandom sequence
        /// </summary>
        /// <returns>A pseudorandom int value.</returns>
        public int NextInt()
        {
            return (int)NextUnsignedLong();
        }

        /// <summary>
        /// Gets a random int in the range minValue to maxValue (inclusive)
        /// </summary>
        /// <returns>A pseudorandom int value</returns>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Max value.</param>
        public int NextInt(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {   
                throw new ArgumentException("min must be less than or equal to max");
            }
                
            double range = (double) ((maxValue - minValue) + 1);
            return (int)(minValue + NextDouble() * range);
        }

        /// <summary>
        /// Gets the next double in the pseudorandom sequence
        /// </summary>
        /// <returns>A pseudorandom double value</returns>
        public double NextDouble()
        { 
            return ((double)NextUnsignedLong() / (double)MaxInt);
        }

        /// <summary>
        /// Gets a random dounble in the range minValue to maxValue
        /// </summary>
        /// <returns>A pseudorandom double</returns>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Max value.</param>
        public double NextDouble(double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * NextDouble();
        }
    }
}

