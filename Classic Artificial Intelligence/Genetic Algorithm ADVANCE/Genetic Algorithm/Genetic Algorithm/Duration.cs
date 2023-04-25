using System;
using System.Collections.Generic;
using System.Text;

namespace Genetic_Algorithm
{
    public class Duration
    {
        public int Begin { get; set; }
        public int End { get; set; }
        public double Fitness { get; set; }

        public bool Belong_Here(int Random)
        {
            if(Random == 1 && Begin == 1)
            {
                return true;
            }
            if (Random <= End && Random >= Begin)
                return true;
            return false;
        }
    }
}
