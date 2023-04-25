using System;
using System.Collections.Generic;
using System.Text;

namespace CARDFS
{
    public class DFS
    {

        public void dfs(CarNode root)
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
                if (temp.Board[2, 5] == 0 && temp.Board[2, 4] == 0 && temp.Board[2, 3] == 0 && temp.Board[2, 2] == 0)
                {
                    int i = 0;
                }
                foreach (CarNode item in temp.Successor())
                {
                    fringe.Push(item);
                }
            }

            if (!solved)
            {
                Console.WriteLine("Unfortunetely Does Not Solved !");
                return;
            }

            //temp.PrintPath();
            Console.WriteLine("Successfully Solved :) \n");
            Console.WriteLine(HashLookUpTable.HashCounter().ToString() + " Seperate Hash Number");

        }
    }
}
