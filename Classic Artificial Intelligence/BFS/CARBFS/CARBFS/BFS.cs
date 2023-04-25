using System;
using System.Collections.Generic;
using System.Text;

namespace CARBFS
{
    public class BFS
    {

        public void bfs(CarNode root)
        {
            LinkedList<CarNode> fringe = new LinkedList<CarNode>();
            fringe.AddLast(root);

            CarNode temp = null;
            bool solved = false;

            while (!(fringe.Count == 0))
            {
                temp = fringe.Last.Value;
                fringe.RemoveLast();
                if (temp.win)
                {
                    solved = true;
                    break;
                }
                if (temp.Board[2,5] == 0 && temp.Board[2,4] == 0 && temp.Board[2,3] == 0 && temp.Board[2,2] == 0)
                {
                    int i = 0;
                }
                foreach(CarNode item in temp.Successor())
                {
                    fringe.AddLast(item);
                }
            }

            if(HashLookUpTable.IsInTable("006770006809116809033309244500200500"))
            {
                int g = 0;
            }

            if (!solved)
            {
                Console.WriteLine("Unfortunetely Does Not Solved !");
                return;
            }

            
            Console.WriteLine("Successfully Solved :) \n");
            Console.WriteLine(HashLookUpTable.HashCounter().ToString() + " Seperate Hash Number");

        }
    }
}
