using System;

namespace Framework
{
    /// <summary>
    /// DTO used for Database Transactions
    /// </summary>
    public class EmotionSpaceDTO
    {
        private double energy;
        private double positivity;

        public EmotionSpaceDTO(double energy, double positivity)
        {
            if (energy > 1 || energy < 0) throw new Exception("Invalid Value for Energy");

            if (positivity > 1 || positivity < 0) throw new Exception("Invalid Value for Positivity");
            this.energy = energy;
            this.positivity = positivity;
        }

        public double Energy
        {
            get
            {
                return energy;
            }
            private set { }
        }
        public double Positivity
        {
            get
            {
                return positivity;
            }
            private set { }
        }
    }
}
