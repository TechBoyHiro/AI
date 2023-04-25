using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CARIDAStar
{
    public class AStar
    {
        public void Astar(CarNode root)
        {
            LinkedList<CarNode> fringe = new LinkedList<CarNode>();
            fringe.AddLast(root);
            int Cutoff = root.Hardibility;
            CarNode temp = null;
            bool solved = false;
            int CutoffCheck = 0;

            while (!(fringe.Count == 0))
            {
                CutoffCheck = GetMinimum(fringe);
                if(CutoffCheck <= Cutoff)
                {
                    temp = fringe.Where(x => x.Hardibility == CutoffCheck).First();
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
                else
                {
                    Console.WriteLine("====== Cutoff : " + Cutoff + " CutoffCheck : " + CutoffCheck);
                    Cutoff = CutoffCheck;
                    fringe = null;
                    fringe = new LinkedList<CarNode>();
                    fringe.AddLast(root);
                    HashLookUpTable.Reset();
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
        public int GetMinimum(LinkedList<CarNode> carNodes)
        {
            int minimum = int.MaxValue;
            foreach(CarNode item in carNodes)
            {
                if(item.Hardibility < minimum)
                {
                    minimum = item.Hardibility;
                }
            }
            return minimum;
        }
       
    }
}
