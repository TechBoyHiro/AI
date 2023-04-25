using System;
using System.Collections.Generic;
using System.Text;

namespace CARBDS
{
    public class BFS
    {
        /// <summary>
        /// BFS Algorithm For Root Model
        /// </summary>
        /// <param name="root"></param>
        public void bfsroot(CarNode root)
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
        /// BFS Algorithm For Goal Model
        /// </summary>
        /// <param name="root"></param>
        public void bfsgoal(GoalCarNode root)
        {
            LinkedList<GoalCarNode> fringe = new LinkedList<GoalCarNode>();
            fringe.AddLast(root);

            GoalCarNode temp = null;
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
                //if (temp.Board[2, 5] == 0 && temp.Board[2, 4] == 0 && temp.Board[2, 3] == 0 && temp.Board[2, 2] == 0)
                //{
                //    int i = 0;
                //}
                foreach (GoalCarNode item in temp.Successor())
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
    }
}
