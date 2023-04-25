using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CARDFS
{
    public enum Direction
    {
        Horizontal = 1,
        Vertical = 2
    }

    public class Car
    {
        public int length { get; set; }
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        public Direction Direction { get; set; }
        // The Main Car Id is 1
        public int CarId { get; set; }

        public Car Copy()
        {
            var result = new Car();
            result.CarId = this.CarId;
            result.length = this.length;
            result.StartRow = this.StartRow;
            result.Direction = this.Direction;
            result.StartColumn = this.StartColumn;
            return result;
        }

        public void MoveRight()
        {
            this.StartColumn++; ;
        }

        public void MoveLeft()
        {
            this.StartColumn--;
        }

        public void MoveUp()
        {
            this.StartRow--;
        }

        public void MoveDown()
        {
            this.StartRow++;
        }
    }


    public class CarNode
    {
        public int[,] Board { get; set; }
        public List<Car> Cars { get; set; }
        public CarNode Parent { get; set; }
        public bool win { get; set; }

        public CarNode(List<Car> cars, CarNode parent)
        {
            Cars = cars;
            Parent = parent;
            Board = new int[6, 6];

            Winner win = HashLookUpTable.AddHash(Inject(Board, Cars));
            if (win != null)
            {
                Console.WriteLine("Time : " + Timer.Stop());
                this.win = true;
                Console.WriteLine("******************************** SOLVED :) *****************************");
                Console.WriteLine("$$$$ INFO => HashNumber : " + win.hashnumber + " - Hash : " + win.hash + " $$$$$$$$$$$$$");
                PrintPath();
                Console.WriteLine("******************************** END *****************************");
                Environment.Exit(0);
            }
        }


        public string Inject(int[,] Board, List<Car> cars)
        {
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

        public List<CarNode> Successor()
        {
            List<CarNode> ToReturn = new List<CarNode>();
            foreach (Car item in Cars)
            {
                if (item.Direction == Direction.Horizontal)
                {
                    if (item.StartColumn + item.length <= 5) // check if it has the ability to goes right
                    {
                        for (int i = item.StartColumn + item.length; i <= 5; i++)
                        {
                            if (Board[item.StartRow, i] == 0) // checking the rightside movement 
                            {
                                List<Car> Copy = new List<Car>(Cars.Select(x => x.Copy()));
                                Copy.Where(x => x.StartColumn == item.StartColumn && x.StartRow == item.StartRow).First().StartColumn = i - (item.length - 1);
                                string temphash = Inject(new int[6, 6], Copy);
                                if (!HashLookUpTable.IsInTable(temphash))
                                    ToReturn.Add(new CarNode(Copy, this));
                            }
                            else break;
                        }
                    }

                    if (item.StartColumn >= 1) // check if it has the ability to goes left
                    {
                        for (int i = item.StartColumn - 1; i >= 0; i--)
                        {
                            if (Board[item.StartRow, i] == 0) // checking the leftside movement 
                            {
                                List<Car> Copy = new List<Car>(Cars.Select(x => x.Copy()));
                                Copy.Where(x => x.StartColumn == item.StartColumn && x.StartRow == item.StartRow).First().StartColumn = (item.StartColumn - (item.StartColumn - i));
                                string temphash = Inject(new int[6, 6], Copy);
                                if (!HashLookUpTable.IsInTable(temphash))
                                    ToReturn.Add(new CarNode(Copy, this));
                            }
                            else break;
                        }
                    }
                }

                else if (item.Direction == Direction.Vertical)
                {
                    if (item.StartRow - (item.length - 1) > 0) // check if it has ability to goes up
                    {
                        for (int i = (item.StartRow - item.length); i >= 0; i--)
                        {
                            if (Board[i, item.StartColumn] == 0) // it can goes up
                            {
                                List<Car> Copy = new List<Car>(Cars.Select(x => x.Copy()));
                                Copy.Where(x => x.StartColumn == item.StartColumn && x.StartRow == item.StartRow).First().StartRow = (item.StartRow - (((item.StartRow - item.length) - i) + 1));
                                string temphash = Inject(new int[6, 6], Copy);
                                if (!HashLookUpTable.IsInTable(temphash))
                                    ToReturn.Add(new CarNode(Copy, this));
                            }
                            else break;
                        }
                    }

                    if (item.StartRow < 5) // check if it has ability to goes down
                    {
                        for (int i = item.StartRow + 1; i <= 5; i++)
                        {
                            if (Board[i, item.StartColumn] == 0) // it can goes down
                            {
                                List<Car> Copy = new List<Car>(Cars.Select(x => x.Copy()));
                                Copy.Where(x => x.StartColumn == item.StartColumn && x.StartRow == item.StartRow).First().StartRow = (item.StartRow + (i - item.StartRow));
                                string temphash = Inject(new int[6, 6], Copy);
                                if (!HashLookUpTable.IsInTable(temphash))
                                    ToReturn.Add(new CarNode(Copy, this));
                            }
                            else break;
                        }
                    }
                }
            }
            ToReturn.Reverse();
            return ToReturn;
        }

        public bool IsFinal()
        {
            bool ToCheck = false; ;
            Inject(Board, Cars);
            for (int i = 0; i < 6; i++)
            {
                if (Board[2, i] == 1) // 1 => Main Car Id
                {
                    for (int j = i; j <= 5; j++)
                    {
                        if (Board[2, j] == 0 || Board[2, j] == 1)
                        {
                            ToCheck = true;
                        }
                        else
                        {
                            ToCheck = false;
                            break;
                        }
                    }
                }
                else continue;
            }
            if (ToCheck)
            {
                int g = 0;
            }
            return ToCheck;
        }

        public void PrintBoard()
        {
            //foreach(Car item in Cars)
            //{
            //    Console.WriteLine("Car Id :" + item.CarId + " | StartRow : " + item.StartRow + " | Start Column: " + item.StartColumn + " | Car Direction: " + item.Direction.ToString() + " | Car Length:" + item.length + "\n");
            //}
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (j == 5)
                    {
                        Console.Write("|\t" + Board[i, j] + "\t|\n");
                        Console.WriteLine("  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __  __");
                    }

                    else
                    {
                        Console.Write("|\t" + Board[i, j] + "\t|");
                    }
                }
            }
        }

        public void PrintPath()
        {
            this.PrintBoard();
            Console.WriteLine("####################################### Node Number : " + Counter.counter + "########################################");
            CarNode parent = Parent;
            while (parent != null)
            {
                parent.PrintBoard();
                Counter.counter++;
                Console.WriteLine("####################################### Node Number : " + Counter.counter + "########################################");
                parent = parent.Parent;
            }
            Console.WriteLine("Counter Is : " + Counter.counter);
        }
    }
}
