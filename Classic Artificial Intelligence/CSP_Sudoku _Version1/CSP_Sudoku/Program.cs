using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;


//PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP


namespace CSP_Sudoku
{
    class Program
    {
        public static Variable[,] Board = new Variable[9, 9];
        public static List<Variable> variables = new List<Variable>();
        public static List<Cage> cages = new List<Cage>();

        static void Main(string[] args)
        {
            Console.WriteLine("CSP Algorithm INITIALIZING :) Sudoku Game (Killer Version)");
            int count = int.Parse(Console.ReadLine());
            List<Temp_Cage> temp_Cages = new List<Temp_Cage>();
            string S = "";
            for (int y=0;y<count;y++)
            {
                S = Console.ReadLine();
                Temp_Cage temp = new Temp_Cage();
                temp.I = new List<int>();
                temp.J = new List<int>();
                int index = S.IndexOf(" ");
                if(index>=16)
                {
                    for (int l = 0; l < 2;l++)
                    {
                        S = S.Remove(0, 1);
                        temp.I.Add(int.Parse(S.Substring(0, 1))-1);
                        S = S.Remove(0, 2);
                        temp.J.Add(int.Parse(S.Substring(0, 1))-1);
                        S = S.Remove(0, 3);
                    }
                    S = S.Remove(0, 1);
                    temp.I.Add(int.Parse(S.Substring(0, 1))-1);
                    S = S.Remove(0, 2);
                    temp.J.Add(int.Parse(S.Substring(0, 1))-1);
                    S = S.Remove(0, 2);
                    temp.Cage_Value = int.Parse(S);
                }
                else
                {
                    S = S.Remove(0, 1);
                    temp.I.Add(int.Parse(S.Substring(0, 1))-1);
                    S = S.Remove(0, 2);
                    temp.J.Add(int.Parse(S.Substring(0, 1))-1);
                    S = S.Remove(0, 4);
                    temp.I.Add(int.Parse(S.Substring(0, 1))-1);
                    S = S.Remove(0, 2);
                    temp.J.Add(int.Parse(S.Substring(0, 1))-1);
                    S = S.Remove(0, 2);
                    temp.Cage_Value = int.Parse(S);
                }
                temp_Cages.Add(temp);
            }
            //Variable[,] Board = new Variable[9, 9];
            for(int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                    Board[i, j] = new Variable();
                    Board[i, j].Domain = new List<int>();
                    Board[i, j].Value = 0;
                    Board[i, j].I = i;
                    Board[i, j].J = j;
                    for (int k = 1; k < 10; k++)
                    {
                        Board[i,j].Domain.Add(k);
                    }
                }
            }
      
            // Removing Unneccussary Values From Variables Domain And Initializing Variables ...
            foreach(Temp_Cage item in temp_Cages)
            {
                for(int number=0;number<item.I.Count;number++)
                {
                    //Board[number, item.J[number]] = new Variable();
                    //Board[number, item.J[number]].Domain = new List<int>();
                    //Board[number, item.J[number]].Value = 0;
                    //Board[number, item.J[number]].I = number;
                    //Board[number, item.J[number]].J = item.J[number];
                    //for (int i=1;i<10;i++)
                    //{
                    //    Board[number, item.J[number]].Domain.Add(i);
                    //}
                    if(item.I.Count == 2) // Execute Only If The Cage Has Two Value Inside ...
                    {
                        if (item.Cage_Value > 10)
                        {
                            for (int i = 1; i <= (item.Cage_Value - 10); i++)
                            {
                                Board[item.I[number], item.J[number]].Domain.Remove(i);
                            }
                        }
                        else
                        {
                            for (int i = item.Cage_Value; i < 10; i++)
                            {
                                Board[item.I[number], item.J[number]].Domain.Remove(i);
                            }
                        }
                    }
                }
            }
            //List<Variable> variables = new List<Variable>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    variables.Add(Board[i, j]);
                }
            }
            foreach (Temp_Cage item in temp_Cages)
            {
                Cage temp = new Cage();
                temp.Cage_Value = item.Cage_Value;
                for(int i=0;i<item.I.Count; i++)
                {
                    temp.variables.Add(variables.Where(x => x.I == item.I[i] && x.J == item.J[i]).First());
                    variables.Where(x => x.I == item.I[i] && x.J == item.J[i]).First().Cage = temp;
                }
            }

            BACKTRACKING_SEARCH();




            while(!AllHasValue(variables))
            {
                variables = variables.OrderBy(x => x.Domain.Count).ToList();
                int index = FindUnAssignedVariable(variables);
                Assign(index);
            }
            Print();
        }

        private static void Print()
        {
            Console.WriteLine("#################### Printing Board ##################");
            for(int i=0;i<10;i++)
            {
                for(int j=0;j<10;j++)
                {
                    Console.Write(variables.Where(x => x.I == i && x.J == j).First().Value + " | ");
                }
                Console.WriteLine("______________________________________________");
            }
        }

        private static bool Assign(int index)
        {
             foreach (int item in variables[index].Domain)
             {
                 bool ST = AllDiffer(index, item);
                 if (ST)
                 {
                    //List<Variable> This_Variables_Version = new List<Variable>();
                    //foreach (Variable item2 in variables)
                    //{
                    //    This_Variables_Version.Add(new Variable { Cage = item2.Cage, Domain = item2.Domain, I = item2.I, J = item2.J, Numbre_Of_Assingments = item2.Numbre_Of_Assingments, Value = item2.Value });
                    //}
                    //Variable[,] This_Board_Version = new Variable[9, 9];
                    //for(int i=0;i<9;i++)
                    //{
                    //    for(int j=0;j<9;j++)
                    //    {
                    //        This_Board_Version[i, j] = new Variable { Cage = Board[i, j].Cage, Domain = Board[i, j].Domain, I = Board[i, j].I, J = Board[i, j].J, Numbre_Of_Assingments = Board[i, j].Numbre_Of_Assingments, Value = Board[i, j].Value };
                    //    }
                    //}
                    //if (Forward_Checking(index, item))
                    //{
                         variables[index].Value = item;
                         Board[variables[index].I, variables[index].J].Value = item;
                         variables[index].Numbre_Of_Assingments += 1;
                         Board[variables[index].I, variables[index].J].Numbre_Of_Assingments += 1;
                         //variables[index].Domain.Remove(item);
                         //Board[variables[index].I, variables[index].J].Domain.Remove(item);
                         if (AllHasValue(variables))
                         {
                             Print();
                             return true;
                         }
                         variables = variables.OrderBy(x => x.Domain.Count).ToList();
                         int empty_index = FindUnAssignedVariable(variables);
                         bool returnvalue = Assign(empty_index);
                         if(!returnvalue)
                         {
                            variables[index].Value = 0;
                            Board[variables[index].I, variables[index].J].Value = 0;
                            //variables = This_Variables_Version;
                            //Board = This_Board_Version;
                         }
                    //}
                    //else
                    //{
                    //    variables = This_Variables_Version;
                    //    Board = This_Board_Version;
                    //}
                 }
             }
            return false;
        }

        private static bool Forward_Checking(int index, int item)
        {
            List<Variable> SameSAT = new List<Variable>();
            List<Variable> SameSOT = new List<Variable>();
            int i = variables[index].I;
            int j = variables[index].J;
            for (int k = 0; k < 9; k++)
            {
                if(k != j)
                    SameSAT.Add(Board[i, k]);
            }
            for (int w = 0; w < 9; w++)
            {
                if(w != i)
                    SameSOT.Add(Board[w, j]);
            }
            int squere = Belongs_To(index);
            List<Variable> SameSquere = GetSameSquere(index, squere);
            if (SameSquere.Count != 8)
                throw new Exception("Should Be 8 :(");
            //bool First_Check = true;
            //for (int z = 0; z < SameSAT.Count; z++)
            //{
            //    if (Board[SameSAT[z].I, SameSAT[z].J].Domain.Count == 0 || Board[SameSOT[z].I, SameSOT[z].J].Domain.Count == 0 || Board[SameSquere[z].I, SameSquere[z].J].Domain.Count == 0)
            //    {
            //        First_Check = false;
            //        break;
            //    }
            //}
            //if(First_Check)
            //{
                for (int z = 0; z < SameSAT.Count; z++)
                {
                    if (Board[SameSAT[z].I, SameSAT[z].J].Value == 0)
                    {
                        Board[SameSAT[z].I, SameSAT[z].J].Domain.Remove(item);
                        variables.Where(x => x.I == SameSAT[z].I && x.J == SameSAT[z].J).First().Domain.Remove(item);
                        if(Board[SameSAT[z].I, SameSAT[z].J].Domain.Count == 0)
                        {
                            return false;
                        }
                    }
                    if (Board[SameSOT[z].I, SameSOT[z].J].Value == 0)
                    {
                        Board[SameSOT[z].I, SameSOT[z].J].Domain.Remove(item);
                        variables.Where(x => x.I == SameSOT[z].I && x.J == SameSOT[z].J).First().Domain.Remove(item);
                        if (Board[SameSOT[z].I, SameSOT[z].J].Domain.Count == 0)
                        {
                            return false;
                        }
                    }
                    if (Board[SameSquere[z].I, SameSquere[z].J].Value == 0)
                    {
                        Board[SameSquere[z].I, SameSquere[z].J].Domain.Remove(item);
                        variables.Where(x => x.I == SameSquere[z].I && x.J == SameSquere[z].J).First().Domain.Remove(item);
                        if(Board[SameSquere[z].I, SameSquere[z].J].Domain.Count == 0)
                        {
                            return false;
                        }
                    }
                }
            return true;
            //}
            //return First_Check;
        }

        private static bool AllDiffer(int index, int item)
        {
            List<int> SameSAT = new List<int>();
            List<int> SameSOT = new List<int>();
            int i = variables[index].I;
            int j = variables[index].J;
            for (int k = 0; k < 9;k++)
            {
                if (Board[i, k].Value != 0)
                    SameSAT.Add(Board[i, k].Value);
            }
            for(int w = 0; w < 9;w++)
            {
                if (Board[w, j].Value != 0)
                    SameSOT.Add(Board[w, j].Value);
            }
            int squere = Belongs_To(index);
            List<Variable> Extraction = GetSameSquere(index, squere);
            List<int> SameSquere = new List<int>();
            foreach(Variable item2 in Extraction)
            {
                if (item2.Value != 0)
                    SameSquere.Add(item2.Value);
            }
            int Cage_Neighbors = 0;
            bool AllAssigned = true;
            if(variables[index].Cage != null)
            {
                foreach (Variable item3 in variables[index].Cage.variables)
                {
                    if (item3.Value != 0)
                    {
                        Cage_Neighbors += item3.Value;
                    }
                    else
                    {
                        AllAssigned = false;
                        break;
                    }
                }
            }
          
            if(variables[index].Cage != null)
            {
                if (AllAssigned)
                {
                    if (SameSAT.Contains(item) || SameSOT.Contains(item) || SameSquere.Contains(item))
                    {
                        return false;
                    }
                    else
                    {
                        if (variables[index].Cage.Cage_Value == Cage_Neighbors)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
                else
                {
                    if (SameSAT.Contains(item) || SameSOT.Contains(item) || SameSquere.Contains(item))
                    {
                        return false;
                    }
                    else
                        return true;
                }
            }
            else
            {
                if (SameSAT.Contains(item) || SameSOT.Contains(item) || SameSquere.Contains(item))
                {
                    return false;
                }
                else
                    return true;
            }
        }

        private static List<Variable> GetSameSquere(int index, int squere)
        {
            int I = variables[index].I;
            int J = variables[index].J;
            List<Variable> to_return = new List<Variable>();
            switch(squere)
            {
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (I == i && J == j)
                            {
                                continue;
                            }
                            else
                                to_return.Add(Board[i, j]);
                        }
                    }
                    break;

                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (I == i && J == j)
                            {
                                continue;
                            }
                            else
                                to_return.Add(Board[i, j]);
                        }
                    }
                    break;

                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (I == i && J == j)
                            {
                                continue;
                            }
                            else
                                to_return.Add(Board[i, j]);
                        }
                    }
                    break;

                case 4:
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (I == i && J == j)
                            {
                                continue;
                            }
                            else
                                to_return.Add(Board[i, j]);
                        }
                    }
                    break;

                case 5:
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (I == i && J == j)
                            {
                                continue;
                            }
                            else
                                to_return.Add(Board[i, j]);
                        }
                    }
                    break;

                case 6:
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (I == i && J == j)
                            {
                                continue;
                            }
                            else
                                to_return.Add(Board[i, j]);
                        }
                    }
                    break;

                case 7:
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (I == i && J == j)
                            {
                                continue;
                            }
                            else
                                to_return.Add(Board[i, j]);
                        }
                    }
                    break;

                case 8:
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (I == i && J == j)
                            {
                                continue;
                            }
                            else
                                to_return.Add(Board[i, j]);
                        }
                    }
                    break;

                case 9:
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (I == i && J == j)
                            {
                                continue;
                            }
                            else
                                to_return.Add(Board[i, j]);
                        }
                    }
                    break;
            }
            return to_return;
        }

        private static int Belongs_To(int index)
        {
            int i = variables[index].I;
            int j = variables[index].J;
            if(i >=0 && i<=2)
            {
                if (j <= 2)
                    return 1;
                else if (j >= 3 && j <= 5)
                    return 2;
                else
                    return 3;
            }
            else if(i>=3 && i<=5)
            {
                if (j <= 2)
                    return 4;
                else if (j <= 5 && j >= 3)
                    return 5;
                else
                    return 6;
            }
            else
            {
                if (j <= 2)
                    return 7;
                else if (j <= 5 && j >= 3)
                    return 8;
                else
                    return 9;
            }
        }

        private static int FindUnAssignedVariable(List<Variable> variables)
        {
            for(int i=0;i<variables.Count;i++)
            {
                if (variables[i].Value == 0)
                    return i;
            }
            return -1;
        }

        public static bool AllHasValue(List<Variable> variables)
        {
            bool check = true;
            foreach(Variable item in variables)
            {
                if (item.Value == 0)
                {
                    check = false;
                    break;
                }
            }
            return check;
        }
    }
}
