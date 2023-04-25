using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    public class Genetic
    {
        public List<float> Fitnesses { get; set; }
        public List<float> COLLABORATE_FITNESSES { get; set; }
        public List<string> Choosen_Chromosome { get; set; }
        public List<Duration> Chromosome_Durations { get; set; }
        public int Generation_Counter { get; set; }

        public Genetic()
        {
            Fitnesses = new List<float>();
            COLLABORATE_FITNESSES = new List<float>();
            Choosen_Chromosome = new List<string>();
            Chromosome_Durations = new List<Duration>();
        }

        public float ALREADY(string chromosome)
        {
            double Fitness = 0;
            char[] Chromosome = chromosome.ToCharArray();
            //char[] SecondPart = Chromosome.Replace(FirstPart.ToString(), "").ToCharArray();
            // For First GeneticTools.CoresNumber the H(N) Would Calcuted Based On Weights Specified Below 
            // W(Priority) = 6
            // W(Cost-Time) = 4
            // W(Upload-Cost) = 2
            // W(Waiting-Time) = 4
            for (int i=0;i<Chromosome.Length;i++)
            {
                double priority = ((GeneticTools.Requests[i].Priority) * (6));
                int UC = (GeneticTools.Max_UploadCost - (GeneticTools.Cores[int.Parse(Chromosome[i].ToString())]).Upload_Cost);
                if (UC > 15)
                {
                    UC *= 10;
                    UC /= GeneticTools.Max_UploadCost;
                }
                double uploadcost = (UC * (4));
                int CT = (GeneticTools.Max_CostTime - GeneticTools.Requests[i].Cost_Time);
                if (CT > 15)
                {
                    CT *= 10;
                    CT /= GeneticTools.Max_CostTime;
                }
                double costtime = (CT * (2));
                int Pwaitingtime = CheckOfWaitingTime(chromosome.Substring(0,i).ToCharArray(),Chromosome[i]);
                double waitingtime = 0;
                if (Pwaitingtime != 0)
                {
                    double SystemTime = Timer.GetTime();
                    if (SystemTime >= Pwaitingtime)
                    {
                        waitingtime = 0;
                    }
                    else
                    {
                        waitingtime = ((Pwaitingtime - SystemTime) * (4));
                    }
                }
                Fitness += (priority + uploadcost + waitingtime + costtime);
            }
            return (float)Fitness;
        }

        public float COLLABORATION(string Chromosome)
        {
            double Fitness = 0;
            char[] chromosome = Chromosome.ToCharArray();
            for (int i = 0; i < Chromosome.Length; i++)
            {
                int CT = (GeneticTools.Max_CostTime - GeneticTools.Requests[i].Cost_Time);
                //if (CT > 10)
                //{
                //    CT *= 10;
                //    CT /= GeneticTools.Max_CostTime;
                //}
                double costtime = (CT * (3));
                int Pwaitingtime = CheckOfWaitingTime(Chromosome.Substring(0, i).ToCharArray(), chromosome[i]);
                double waitingtime = 0;
                if (Pwaitingtime != 0)
                {
                    double SystemTime = Timer.GetTime();
                    if (SystemTime >= Pwaitingtime)
                    {
                        waitingtime = 0;
                    }
                    else
                    {
                        waitingtime = ((Pwaitingtime - SystemTime) * (3));
                    }
                }
                Fitness += (waitingtime + costtime);
            }
            return (float)Fitness;
        }

        private int CheckOfWaitingTime(char[] firstPart, char v)
        {
            for(int i=0;i<firstPart.Length;i++)
            {
                if(firstPart[i] == v)
                {
                    int TimeNeeded = GeneticTools.Requests[i].EntryTime + GeneticTools.Requests[i].Cost_Time;
                    return TimeNeeded;
                }
            }
            return 0;
        }

        public void Selection()
        {
            float Fitness_Sum = 0;
            foreach(string Chromosome in GeneticTools.Population)
            {
                float temp = COLLABORATION(Chromosome);
                COLLABORATE_FITNESSES.Add(temp);
                Fitnesses.Add(F1SCORE(temp,ALREADY(Chromosome)));
                Fitness_Sum += temp;
            }
            WORSTGEN();
            int last = 0;
            for(int i=0;i<GeneticTools.Population.Count;i++)
            {
                Duration temp = new Duration();
                temp.Fitness = ((Fitnesses[i] / Fitness_Sum) * (100));
                temp.Begin = (last + 1);
                temp.End = (int)(last + temp.Fitness + 1);
                last += ((int)temp.Fitness+1);
                Chromosome_Durations.Add(temp);
            }
            // EveryThing Done Up Until Here Is To Initialize The Fitness Value And It's Duration For Every Single Chromosome ...
            Random random = new Random();
            while(GeneticTools.Children.Count != GeneticTools.Population.Count)
            {
                // From Now On Our Job Is To Select Chromosomes Randomly And Do the Cross_Over And Mutation ...
                for (int i = 0; i < 4; i++)
                {
                    int Next = random.Next(1, Chromosome_Durations[GeneticTools.Child_Number-1].End);
                    for (int j = 0; j < Chromosome_Durations.Count; j++)
                    {
                        if (Chromosome_Durations[j].Belong_Here(Next))
                        {
                            Choosen_Chromosome.Add(GeneticTools.Population[j]);
                            break;
                        }
                    }
                }
                if (Choosen_Chromosome.Count == 4)
                {
                    Cross_Over(Choosen_Chromosome[0], Choosen_Chromosome[2]);
                    Cross_Over(Choosen_Chromosome[1], Choosen_Chromosome[3]);
                }
                Choosen_Chromosome.RemoveRange(0, 4);
            }
            //Mutation();
        }

        private float F1SCORE(float temp, float v)
        {
            float multiplication = temp * v;
            float sum = temp + v;
            float divide = multiplication / sum;
            return (2 * divide);
        }

        private void WORSTGEN()
        {
            List<float> Fitnesses_Copy = new List<float>();
            for(int i=0;i<COLLABORATE_FITNESSES.Count;i++)
            {
                Fitnesses_Copy.Add(COLLABORATE_FITNESSES[i]);
            }
            // Removing 10 Worst Gen
            //List<string> Fitnesses_Copy = new List<string>();
            //for(int i=0;i<Fitnesses.Count;i++)
            //{
            //    Population_Copy.Add(GeneticTools.Population[i]);
            //}
            int[] Indexes = new int[20];
            for(int i=0;i<20;i++)
            {
                Indexes[i] = FIND_INDEX(Fitnesses_Copy);
                Fitnesses_Copy.RemoveAt(Indexes[i]);
            }
            for(int j=0;j<20;j++)
            {
                string Gen = GeneticTools.Population[Indexes[j]];
                int STR_INDEX = FIND_WORST_GEN(Gen);
                Gen = Gen.Remove(STR_INDEX, 1);
                GeneticTools.Population[Indexes[j]] = Gen;
                //float temp = COLLABORATION(Gen);
                //COLLABORATE_FITNESSES[Indexes[j]] = temp;
            }
        }

        private int FIND_WORST_GEN(string Gen)
        {
            //char[] gen = Gen.ToCharArray();
            int[,] maximums = new int[2,3];
            int index = 0;
            int maximum_upload_cost = 0;
            for(int j=0;j<2;j++)
            {
                for (int i = 0; i < Gen.Length; i++)
                {
                    if (GeneticTools.Requests[i].Cost_Time > maximum_upload_cost)
                    {
                        maximum_upload_cost = GeneticTools.Requests[i].Cost_Time;
                        index = i;
                    }
                }
                maximums[j, 0] = maximum_upload_cost;
                maximums[j, 1] = GeneticTools.Requests[index].Priority;
                maximums[j, 2] = index;
                Gen = Gen.Remove(index, 1);
                maximum_upload_cost = 0;
                index = 0;
            }
            if (maximums[0, 1] > maximums[1, 1])
                return maximums[1, 2];
            else
                return maximums[0, 2];
        }

        private int FIND_INDEX(List<float> FItnesses)
        {
            float Worst = int.MaxValue;
            int index = 0;
            int generation_limit = GeneticTools.Child_Number / 20;
            int max_lenght = GeneticTools.RequestsNumber - (Generation_Counter/generation_limit);
            for(int i=0;i<FItnesses.Count;i++)
            {
                if (GeneticTools.Population[i].Length >= max_lenght)
                {
                    if (FItnesses[i] <= Worst)
                    {
                        Worst = FItnesses[i];
                        index = i;
                    }
                }
            }
            return index;
        }

        private void Mutation()
        {
            int Number_To_Mutate = (int)(GeneticTools.Mutate_Rate * GeneticTools.Children.Count);
            Random random = new Random();
            for(int i=0;i<Number_To_Mutate;i++)
            {
                int Next = random.Next(0, GeneticTools.Children.Count - 1);
                string selected = GeneticTools.Children[Next];
                //int Cut_Line = random.Next(0, selected.Length - 1);
                //int Random_Gen = random.Next(0, GeneticTools.CoresNumber - 1);
                //selected[Cut_Line] = (char)Random_Gen;
                int maximum = 0;
                int index = 0;
                for(int j=0;j<selected.Length;j++)
                {
                    if(GeneticTools.Requests[j].Cost_Time > maximum)
                    {
                        maximum = GeneticTools.Requests[j].Cost_Time;
                        index = j;
                    }
                }
                selected = selected.Remove(index, 1);
                GeneticTools.Children[Next] = selected;
            }
        }

        private void Cross_Over(string v1, string v2)
        {
            Random random = new Random();
            int Cut_Line = 0;
            if(v1.Length > v2.Length)
            {
                Cut_Line = random.Next(1, v2.Length-1);
            }
            else
            {
                Cut_Line = random.Next(1, v1.Length-1);
            }
            string First_v1 = v1.Substring(0, Cut_Line);
            v1 = v1.Remove(0, Cut_Line);
            string Second_v1 = v1;
            string First_v2 = v2.Substring(0, Cut_Line);
            v2 = v2.Remove(0, Cut_Line);
            string Second_v2 = v2;
            string First_Child = First_v1 + Second_v2;
            string Second_Child = First_v2 + Second_v1;
            GeneticTools.Children.Add(First_Child);
            GeneticTools.Children.Add(Second_Child);
        }

        public void Genetic_Algorithm(int Generation)
        {
            Generation_Counter = 0;
            while(Generation_Counter != Generation)
            {
                Selection();
                Console.WriteLine("Population           Fitness     Generation");
                double FitnessSum = 0;
                for (int j = 0; j < GeneticTools.Population.Count; j++)
                {
                    Console.WriteLine(GeneticTools.Population[j] + "\t\t\t" + Fitnesses[j] + "\t\t" + Generation_Counter);
                    FitnessSum += Fitnesses[j];
                }
                Console.WriteLine("The Average Fitness : " + (float)(FitnessSum / Fitnesses.Count));
                if (!(Generation_Counter + 1 == Generation))
                {
                    this.Fitnesses.RemoveRange(0, Fitnesses.Count);
                    this.Chromosome_Durations.RemoveRange(0, Chromosome_Durations.Count);
                    this.Choosen_Chromosome.RemoveRange(0, Choosen_Chromosome.Count);
                    this.COLLABORATE_FITNESSES.RemoveRange(0, COLLABORATE_FITNESSES.Count);
                }
                //GeneticTools.Population = GeneticTools.Children;
                for(int i=0;i<GeneticTools.Population.Count;i++)
                {
                    GeneticTools.Population[i] = GeneticTools.Children[i];
                }
                GeneticTools.Children.RemoveRange(0, GeneticTools.Children.Count);
                Generation_Counter++;
            }
            Console.WriteLine("The {0} Generation Was Successfully Created :)",Generation_Counter);
            int generation_limit2 = GeneticTools.Child_Number / 10;
            int minimum_char = GeneticTools.RequestsNumber - (Generation_Counter / generation_limit2);
            //int minimum_char = int.MaxValue;
            //for (int i = 0; i < Fitnesses.Count; i++)
            //{
            //    if (GeneticTools.Population[i].Length <= minimum_char)
            //    {
            //        minimum_char = GeneticTools.Population[i].Length;
            //    }
            //}
            float Max = -1000;
            for(int i=0;i<Fitnesses.Count;i++)
            {
                if(Fitnesses[i]>=Max)
                {
                    if(GeneticTools.Population[i].Length <= minimum_char)
                    {
                        Max = Fitnesses[i];
                    }
                }
            }
            int index = Fitnesses.IndexOf(Fitnesses.Where(x => x == Max).First());
            Console.WriteLine("The Best Chromosome is : {0} With Fitness Of {1}", GeneticTools.Population[index], Max);
            Console.WriteLine("The Time It Cost is : " + Timer.GetTime() + " Milisecond");
            Timer.Stop();
        }
    }
}
