using System;
using System.Collections.Generic;
using System.Text;

namespace Genetic_Algorithm
{
    public class Request
    {
        public int Request_Id { get; set; }
        public int Priority { get; set; }
        public int EntryTime { get; set; }
        public int Cost_Time { get; set; }
        public float Fitness { get; set; }
    }
}
