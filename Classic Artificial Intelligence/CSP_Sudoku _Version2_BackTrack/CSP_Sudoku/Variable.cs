using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSP_Sudoku_BackTrack
{
    public class Variable
    {
        public List<int> Domain { get; set; }
        [Range(1,9)]
        public int Value { get; set; }
        public int Numbre_Of_Assingments { get; set; }
        public Cage Cage { get; set; }
        public int I { get; set; }
        public int J { get; set; }
    }
}
