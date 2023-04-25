using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_Sudoku_BackTrack_ForwardChecking_LCV
{
    public class Forward_Checking_Respond
    {
        public Forward_Checking_Respond()
        {
            Deleted_From = new List<Variable>();
        }
        public bool Result { get; set; }
        public List<Variable> Deleted_From { get; set; }
    }
}
