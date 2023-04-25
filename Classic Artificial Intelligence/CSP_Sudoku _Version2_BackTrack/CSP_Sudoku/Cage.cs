using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_Sudoku_BackTrack
{
    public class Cage
    {
        public Cage()
        {
            variables = new List<Variable>();
        }
        public List<Variable> variables { get; set; }
        public int Cage_Value;
    }
}
