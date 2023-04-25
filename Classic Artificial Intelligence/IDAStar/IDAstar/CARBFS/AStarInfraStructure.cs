using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CARIDAStar
{
    public static class AStarInfraStructure
    {
        /// <summary>
        /// check how many steps the cars differ from their own place in goal state
        /// </summary>
        /// <param name="cars"></param>
        /// <returns>G(n) + K(n)</returns>
        public static int Heuristic(List<Car> cars)
        {
            int Differ=0;

            foreach(Car item in GoalStatistics.Cars)
            {
                Car temp = cars.Where(x => x.CarId == item.CarId).First();

                if(temp.Direction == Direction.Horizontal)
                {
                    if(temp.StartColumn == item.StartColumn)
                    {
                        continue;
                    }
                    else
                    {
                        Differ++;
                        if(temp.StartColumn > item.StartColumn)
                        {
                            Differ += (temp.StartColumn - item.StartColumn);  // we add the differ location of the car from its place in goal
                        }
                        else
                        {
                            Differ += (item.StartColumn - temp.StartColumn);
                        }
                    }
                }
                else
                {
                    if (temp.StartRow == item.StartRow)
                    {
                        continue;
                    }
                    else
                    {
                        Differ++; // we get that a car is not in specified place
                        if (temp.StartRow > item.StartRow)
                        {
                            Differ += (temp.StartRow - item.StartRow); // we add the differ location of the car from its place in goal
                        }
                        else
                        {
                            Differ += (item.StartRow - temp.StartRow);
                        }
                    }
                }
            }
            return Differ;
        }
    }
}
