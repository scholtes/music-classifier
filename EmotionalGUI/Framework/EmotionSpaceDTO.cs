using System;


namespace Framework
{
    class EmotionSpaceDTO
    {
        double intensity
        {
            get
            {
                return intensity;
            }
            set
            {
                if(value > 1 || value < 0)
                {
                    throw new Exception("Invalid value for intensity");
                }
                intensity = value;
            }
        }

        double positivity
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
    }
}
