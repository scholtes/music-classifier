using System;

namespace Framework
{
    /// <summary>
    /// DTO used for Database Transactions
    /// </summary>
    public class EmotionSpaceDTO
    {
        public double Energy
        {
            get
            {
                return _energy;
            }
            set
            {
                if(value > 1 || value < 0)
                {
                    throw new Exception("Invalid value for energy");
                }
                _energy = value;
            }
        }
        public double Positivity
        {
            get
            {
                return _positivity;
            }
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new Exception("Invalid value for positivity");
                }
                _positivity = value;
            }
        }

        private double _energy;
        private double _positivity;

    }
}
