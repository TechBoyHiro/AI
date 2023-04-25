using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CARAStar_Without_Goal
{
    public class AStar
    {
        public void Astar(CarNode root)
        {
            LinkedList<CarNode> fringe = new LinkedList<CarNode>();
            fringe.AddLast(root);

            CarNode temp = null;
            bool solved = false;

            while (!(fringe.Count == 0))
            {
                temp = GetMinimum(fringe);
                fringe.Remove(temp);
                if (temp.win)
                {
                    solved = true;
                    break;
                }
                //if (temp.Board[2, 5] == 0 && temp.Board[2, 4] == 0 && temp.Board[2, 3] == 0 && temp.Board[2, 2] == 0)
                //{
                //    int i = 0;
                //}
                foreach (CarNode item in temp.Successor())
                {
                    fringe.AddLast(item);
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
                if(item.Hardibility < minimum)
                {
                    minimum = item.Hardibility;
                }
                if(item.Hardibility > maximum)
                {
                    maximum = item.Hardibility;
                }
            }
            HeuristicInfrastructure.MAX_HEURISTIC = maximum;
            Console.WriteLine("Maximum F(N) = " + maximum);
            return carNodes.Where(x => x.Hardibility == minimum).First();
        }
       
    }
}
