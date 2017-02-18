using System;

namespace Framework
{
    /// <summary>
    /// DTO used for Database Transactions
    /// </summary>
    public class EmotionSpaceDTO
    {

        public EmotionSpaceDTO()
            :this(.5,.5)
        {
            //Center the point
        }

        public EmotionSpaceDTO(double energy, double Positivity)
        {
            this.Energy = energy;
            this.Positivity = Positivity;
        }

        public double Energy
        {
            get
            {
                return energy;
            }
            set
            {
                if(value > 1 || value < 0)
                {
                    throw new Exception("Invalid value for energy");
                }
                energy = value;
            }
        }
        public double Positivity
        {
            get
            {
                return positivity;
            }
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new Exception("Invalid value for positivity");
                }
                positivity = value;
            }
        }

        private double energy;
        private double positivity;

    }
}
