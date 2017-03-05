using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class EmotionDTO
    {
        public string Name;
        public double Energy;
        public double Positivity;

        public EmotionDTO(string name, double energy, double positivity)
        {
            Name = name;
            Energy = energy;
            Positivity = positivity;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
