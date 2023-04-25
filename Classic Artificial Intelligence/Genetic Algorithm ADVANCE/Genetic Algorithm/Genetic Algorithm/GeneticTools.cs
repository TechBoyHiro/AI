using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    public static class GeneticTools
    {
        public static List<Core> Cores { get; set; }
        public static List<Request> Requests { get; set; }
        public static int CoresNumber { get; set; }
        public static int RequestsNumber { get; set; }
        public static List<Request> DoneRequests { get; set; }
        public static List<Core> BusyCores { get; set; }
        public static List<string> Population { get; set; }
        public static List<string> Children { get; set; }
        public static float Mutate_Rate { get; set; }
        public static int Child_Number { get; set; }
        public static int Max_UploadCost { get; set; }
        public static int Max_CostTime { get; set; }

        public static void InitializePopulation()
        {
            List<Core> TempCores = new List<Core>();
            Random random = new Random();
            // Initialize 20 First Parent-Generation
            for(int i=0;i<Child_Number;i++)
            {
                for (int k = 0; k < Cores.Count; k++)
                {
                    TempCores.Add(Cores[k]);
                }
                string Chromosome = "";
                for (int j=0;j<CoresNumber;j++)
                {
                    int index = random.Next(0, (TempCores.Count-1));
                    Chromosome = Chromosome + TempCores[index].Core_Id.ToString();
                    TempCores.Remove(TempCores[index]);
                }
                for(int k=0;k<RequestsNumber-CoresNumber;k++)
                {
                    Chromosome = Chromosome + random.Next(0, Cores.Count).ToString();
                }
                Population.Add(Chromosome);
            }
        }
    }
}
