
namespace Framework
{
    /// <summary>
    /// Class used to hold precise values for fractions
    /// </summary>
    public class Fraction
    {
        public int numerator;
        public int denominator;

        /// <summary>
        /// Returns an estimate of the fraction
        /// </summary>
        /// <returns>A double</returns>
        public double estimateFraction()
        {
            return (double)numerator / (double)denominator;
        }
    }
}