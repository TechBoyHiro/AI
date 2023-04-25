using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Genetic Algorithm ADVANCE :) ");
            // Initialize the GeneticTools Properties
            GeneticTools.Requests = new List<Request>();
            GeneticTools.Cores = new List<Core>();
            GeneticTools.DoneRequests = new List<Request>();
            GeneticTools.BusyCores = new List<Core>();
            GeneticTools.Population = new List<string>();
            GeneticTools.Children = new List<string>();
            List<Request> TempRequests = new List<Request>();
            string coreandrequestnumber = Console.ReadLine();
            int temp1 = coreandrequestnumber.IndexOf(" ");
            GeneticTools.CoresNumber = int.Parse(coreandrequestnumber.Substring(0, temp1 + 1));
            coreandrequestnumber = coreandrequestnumber.Remove(0, temp1 + 1);
            if(coreandrequestnumber.Length >=2)
            {
                GeneticTools.RequestsNumber = int.Parse(coreandrequestnumber.Substring(0, temp1+1));
            }
            else
            {
                GeneticTools.RequestsNumber = int.Parse(coreandrequestnumber.Substring(0, temp1));
            }
            
            for(int i=0; i<GeneticTools.RequestsNumber;i++)
            {       // Getting Requests
                string temp2 = Console.ReadLine();
                int temp3 = temp2.IndexOf(" ");
                int priority = int.Parse(temp2.Substring(0, temp3 + 1));
                temp2 = temp2.Remove(0, temp3 + 1);
                int temp4 = temp2.IndexOf(" ");
                int entrytime = int.Parse(temp2.Substring(0, temp4 + 1));
                temp2 = temp2.Remove(0, temp4 + 1);
                int costtime = int.Parse(temp2);
                Request temp5 = new Request();
                temp5.Priority = priority;
                temp5.EntryTime = entrytime;
                temp5.Cost_Time = costtime;
                TempRequests.Add(temp5);
                //GeneticTools.Requests.Add(temp5);
            }

            for(int i=0; i<GeneticTools.CoresNumber;i++)
            {       // Getting Cores
                Core temp = new Core();
                temp.Core_Id = i;
                temp.Upload_Cost = int.Parse(Console.ReadLine());
                GeneticTools.Cores.Add(temp);
            }
            GeneticTools.Requests = TempRequests.OrderBy(x => x.Priority).ToList();
            int max_cost = -1;
            foreach(Request item in GeneticTools.Requests)
            {
                if (item.Cost_Time >= max_cost)
                    max_cost = item.Cost_Time;
            }
            int max_upload_cost = -1;
            foreach(Core item in GeneticTools.Cores)
            {
                if (item.Upload_Cost >= max_upload_cost)
                    max_upload_cost = item.Upload_Cost;
            }
            GeneticTools.Max_CostTime = max_cost;
            GeneticTools.Max_UploadCost = max_upload_cost;
            GeneticTools.Child_Number = 100;    // How Many Children It Should Create ...
            GeneticTools.Mutate_Rate = (float)0.4;
            Timer.Start();
            GeneticTools.InitializePopulation();
            Genetic GeneticAlgorithm = new Genetic();
            GeneticAlgorithm.Genetic_Algorithm(25);  // How Many Generation It Should Create ...
            
        }

    }
}
