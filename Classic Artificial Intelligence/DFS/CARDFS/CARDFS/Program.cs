using System;
using System.Collections.Generic;

namespace CARDFS
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer.Start();
            Console.WriteLine("Car Blocking Game DFS Version :)");
            Console.WriteLine("Enter the number of cars");
            List<Car> Cars = new List<Car>();
            int counter = int.Parse(Console.ReadLine());
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
            //Console.WriteLine("**********************************************************");

            //foreach (Car item in Cars)
            //{
            //    Console.WriteLine(item.CarId + "-" + item.StartRow + "-" + item.StartColumn + "-" + item.length + "-" + item.Direction.ToString());
            //}
            CarNode RootNode = new CarNode(Cars, null);
            DFS BFS = new DFS();
            BFS.dfs(RootNode);

        }
    }
}
