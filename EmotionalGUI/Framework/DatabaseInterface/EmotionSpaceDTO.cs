using System;

namespace Framework
{
    /// <summary>
    /// DTO used for Database Transactions
    /// </summary>
    public class EmotionSpaceDTO
    {
        double Energy
        {
            get
            {
                return Energy;
            }
            set
            {
                if(value > 1 || value < 0)
                {
                    throw new Exception("Invalid value for energy");
                }
                Energy = value;
            }
        }
        double Positivity
        {
            get
            {
                return Positivity;
            }
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new Exception("Invalid value for positivity");
                }
                Positivity = value;
            }
        }
    }
}
