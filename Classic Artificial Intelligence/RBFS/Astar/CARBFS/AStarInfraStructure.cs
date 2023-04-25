using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBFS
{
    public static class AStarInfraStructure
    {
        /// <summary>
        /// check how many steps the cars differ from their own place in goal state
        /// </summary>
        /// <param name="cars"></param>
        /// <returns>G(n) + K(n)</returns>
        public static int UnSortedCar { get; set; }
        public static int Heuristic(List<Car> cars)
        {
            int Difficulty = 0;
            UnSortedCar = 0;
            Car Main = cars.Where(x => x.CarId == 1).First();
            List<Car> Verticals = new List<Car>();
            foreach(Car item in cars)
            {
                if(item.Direction == Direction.Vertical)
                {
                    if(item.StartColumn == (Main.StartColumn+Main.length))
                    {
                        if(item.StartRow<=2)
                        {
                            UnSortedCar++;
                            Difficulty++;
                            Verticals.Add(item);
                        }
                    }
                    
                }
                else
                {
                    if(Verticals.Count != 0)
                    {
                        if(item.length == 3 && item.StartColumn >= 0 && Verticals.Count != 0)
                        {
                            UnSortedCar++;
                        }

                        foreach(Car vertical_item in Verticals)
                        {
                            if (item.StartRow > vertical_item.StartRow)
                            {
                                if(item.StartColumn < vertical_item.StartColumn && vertical_item.StartColumn < (item.StartColumn+item.length))
                                {
                                    Difficulty += ((item.StartColumn+2)+(6-vertical_item.StartRow));
                                }
                            }
                        }
                    }
                }
            }
            return Difficulty + UnSortedCar;
        }
    }
}
