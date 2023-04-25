using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RBFS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Car Blocking Game RBFS New Version :)");
            Console.WriteLine("Enter the number of cars For Root Search");
            List<Car> Cars = new List<Car>();
            int counter = int.Parse(Console.ReadLine());
            HeuristicInfrastructure.Cars_Number = counter;
            for (int i = 0; i < counter; i++)
            {
                Car temp = new Car();
                string enter = Console.ReadLine();

                // read carid 
                int gg = enter.IndexOf(" ");
                temp.CarId = int.Parse(enter.Substring(0, gg));
                enter = enter.Remove(0, gg + 1);

                // read startrow
                int hh = enter.IndexOf(" ");
                temp.StartRow = int.Parse(enter.Substring(0, hh)) - 1;
                enter = enter.Remove(0, hh + 1);

                // read startcolumn
                int ee = enter.IndexOf(" ");
                temp.StartColumn = int.Parse(enter.Substring(0, ee)) - 1;
                enter = enter.Remove(0, ee + 1);

                // read h/v
                int ww = enter.IndexOf(" ");
                if (enter.Substring(0, ww) == "h")
                {
                    temp.Direction = Direction.Horizontal;
                }
                else
                {
                    temp.Direction = Direction.Vertical;
                }
                enter = enter.Remove(0, ww + 1);

                // read length
                //int qq = enter.IndexOf(" ");
                temp.length = int.Parse(enter.Substring(0));
                //enter = enter.Replace(enter.Substring(0, qq), ""); 
                if (temp.Direction == Direction.Vertical)
                {
                    temp.StartRow = temp.StartRow + (temp.length - 1);
                }
                Cars.Add(temp);
            }


            //Console.WriteLine("Enter the number of cars For Goal Search");
            //List<Car> Cars2 = new List<Car>();
            //int counter2 = int.Parse(Console.ReadLine());
            //for (int i = 0; i < counter2; i++)
            //{
            //    Car temp = new Car();
            //    string enter = Console.ReadLine();

            //    // read carid 
            //    int gg = enter.IndexOf(" ");
            //    temp.CarId = int.Parse(enter.Substring(0, gg));
            //    enter = enter.Remove(0, gg + 1);

            //    // read startrow
            //    int hh = enter.IndexOf(" ");
            //    temp.StartRow = int.Parse(enter.Substring(0, hh)) - 1;
            //    enter = enter.Remove(0, hh + 1);

            //    // read startcolumn
            //    int ee = enter.IndexOf(" ");
            //    temp.StartColumn = int.Parse(enter.Substring(0, ee)) - 1;
            //    enter = enter.Remove(0, ee + 1);

            //    // read h/v
            //    int ww = enter.IndexOf(" ");
            //    if (enter.Substring(0, ww) == "h")
            //    {
            //        temp.Direction = Direction.Horizontal;
            //    }
            //    else
            //    {
            //        temp.Direction = Direction.Vertical;
            //    }
            //    enter = enter.Remove(0, ww + 1);

            //    // read length
            //    //int qq = enter.IndexOf(" ");
            //    temp.length = int.Parse(enter.Substring(0));
            //    //enter = enter.Replace(enter.Substring(0, qq), ""); 
            //    if (temp.Direction == Direction.Vertical)
            //    {
            //        temp.StartRow = temp.StartRow + (temp.length - 1);
            //    }
            //    Cars2.Add(temp);
            //}


            //GoalStatistics.Cars = Cars2;
            //GoalStatistics.GoalHash = GetHash(Cars2);
            int RootHardibility = AStarInfraStructure.Heuristic(Cars);
            CarNode RootNode = new CarNode(Cars,null,RootHardibility);
            RBFS AStar = new RBFS();
            Timer.Start();
            AStar.Rbfs(RootNode);
        }

        public static string GetHash(List<Car> cars)
        {
            int[,] Board = new int[6, 6];

            foreach (Car item in cars)
            {
                if (item.Direction == Direction.Horizontal)
                {
                    for (int i = 0; i < item.length; i++)
                    {
                        int temp1 = item.StartRow;
                        int temp2 = item.StartColumn + i;
                        Board[temp1, temp2] = item.CarId;
                    }
                }
                else
                {
                    for (int i = 0; i < item.length; i++)
                        Board[item.StartRow - i, item.StartColumn] = item.CarId;
                }
            }
            // create a specific hash number for this board [36 number]
            string hash = "";
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    hash = hash.ToString() + Board[i, j].ToString();
                }
            }
            return hash;
        }
    }
}
