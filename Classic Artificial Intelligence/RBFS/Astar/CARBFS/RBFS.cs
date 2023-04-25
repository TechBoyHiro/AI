using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBFS
{
    public class RBFS
    {
        public void Rbfs(CarNode root)
        {
            LinkedList<CarNode> fringe = new LinkedList<CarNode>();
            LinkedList<CarNode> MainFringe = new LinkedList<CarNode>();
            //fringe.AddLast(root);
            fringe.AddLast(root);
            CarNode temp = null;
            bool solved = false;
            if(root.win)
            {
                solved = true;
            }
            else
            {
                foreach(CarNode item in root.Successor())
                {
                    fringe.AddLast(item);
                }
            }
            while (!(fringe.Count == 0) && !solved)
            {
                CarNode temp3 = GetMinimum(fringe);
                if(temp3.win)
                {
                    solved = true;
                    break;
                }
                MainFringe.AddLast(temp3);
                fringe.Remove(temp3);
                CarNode SecondChance = GetMinimum(fringe);
                //fringe.AddFirst(temp3);

                while (!(MainFringe.Count == 0) && !solved)
                {
                    temp = GetMinimum(MainFringe);
                    
                    if (temp.win)
                    {
                        solved = true;
                        break;
                    }
                    if(temp.F <= SecondChance.F)
                    {
                        List<CarNode> tempnode = new List<CarNode>();
                        foreach (CarNode item in temp.Successor())
                        {
                            tempnode.Add(item);
                        }
                        if (tempnode.Count != 0)
                        {
                            if (GetMinimum(tempnode).F <= temp.F)
                            {
                                foreach (CarNode item in tempnode)
                                {
                                    fringe.AddLast(item);
                                }
                                MainFringe.Remove(temp);
                                MainFringe.AddLast(GetMinimum(tempnode));
                                SecondChance = GetMinimum(fringe);
                            }
                            else
                            {
                                temp.F = GetMinimum(tempnode).F;
                                fringe.AddLast(temp);
                                break;
                            }
                        }
                        else
                            MainFringe.Remove(temp);
                    }
                    
                }
            }

            if (!solved)
            {
                Console.WriteLine("Unfortunetely Does Not Solved !");
                return;
            }


            Console.WriteLine("Successfully Solved :) \n");
            Console.WriteLine(HashLookUpTable.HashCounter().ToString() + " Seperate Hash Number");

        }
        /// <summary>
        /// return the least minimum hardibility Node From Fringe
        /// </summary>
        /// <param name="carNodes"></param>
        /// <returns></returns>
        public CarNode GetMinimum(LinkedList<CarNode> carNodes)
        {
            int minimum = int.MaxValue;
            int maximum = 0;
            foreach(CarNode item in carNodes)
            {
                if(item.F < minimum)
                {
                    minimum = item.F;
                }
                if(item.F > maximum)
                {
                    maximum = item.F;
                }
            }
            HeuristicInfrastructure.MAX_HEURISTIC = maximum;
            Console.WriteLine("Maximum F(N) = " + maximum);
            return carNodes.Where(x => x.F == minimum).First();
        }

        public CarNode GetMinimum(List<CarNode> carNodes)
        {
            int minimum = int.MaxValue;
            int maximum = 0;
            foreach (CarNode item in carNodes)
            {
                if (item.F < minimum)
                {
                    minimum = item.F;
                }
                if (item.F > maximum)
                {
                    maximum = item.F;
                }
            }
            HeuristicInfrastructure.MAX_HEURISTIC = maximum;
            Console.WriteLine("Maximum F(N) = " + maximum);
            return carNodes.Where(x => x.F == minimum).First();
        }

    }
}
