using System;
using System.Collections.Generic;
using System.Text;

namespace CARIDS
{
    public class DFS
    {
        public bool dfs(CarNode root, int Depth)
        {
            Stack<CarNode> fringe = new Stack<CarNode>();
            fringe.Push(root);

            CarNode temp = null;
            bool solved = false;

            while (!(fringe.Count == 0))
            {
                temp = fringe.Pop();
                if (temp.win)
                {
                    solved = true;
                    break;
                }
                //if (temp.Board[2, 5] == 0 && temp.Board[2, 4] == 0 && temp.Board[2, 3] == 0 && temp.Board[2, 2] == 0)
                //{
                //    int i = 0;
                //}
                if (Depth == 49 && HashLookUpTable.HashCounter() > 202)
                {
                    if (HashLookUpTable.IsInTable("305022305000311000777600000600444600"))
                    {
                        int s = 0;
                    }
                }
                if (!temp.IsInDepthOf(Depth))
                {
                    foreach (CarNode item in temp.Successor())
                    {
                        fringe.Push(item);
                    }
                }
                else if (fringe.Count == 0)
                {
                    return false;
                }
            }

            if (!solved)
            {
                Console.WriteLine("Unfortunetely Does Not Solved !");
                return false;
            }
            Console.WriteLine("Time : " + Environment.TickCount);
            //temp.PrintPath();
            Console.WriteLine("Successfully Solved :) \n");
            Console.WriteLine(HashLookUpTable.HashCounter().ToString() + " Seperate Hash Number");
            return true;

        }

    }
}
