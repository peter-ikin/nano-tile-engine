using System;

namespace Nano.Engine.Sys
{
    public interface IRandom
    {
        /// <summary>
        /// Gets a random unsigned long.
        /// </summary>
        /// <returns>a pseudoandom ulong.</returns>
        ulong NextUnsignedLong();

        /// <summary>
        /// Gets a random double in the range 0 to 1
        /// </summary>
        /// <returns>The double.</returns>
        double NextDouble();

        /// <summary>
        /// Gets a random dounble in the range minValue to maxValue
        /// </summary>
        /// <returns>A pseudorandom double</returns>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Max value.</param>
        double NextDouble(double minValue, double maxValue);

        /// <summary>
        /// Gets a random int
        /// </summary>
        /// <returns>A pseudorandom int</returns>
        int NextInt();

        /// <summary>
        /// Gets a random int in the range minValue to maxValue (inclusive)
        /// </summary>
        /// <returns>A pseudorandom int</returns>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Max value.</param>
        int NextInt(int minValue, int maxValue);
    }
}

