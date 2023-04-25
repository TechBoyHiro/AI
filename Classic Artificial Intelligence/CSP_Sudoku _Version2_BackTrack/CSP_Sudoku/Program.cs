using System;
using System.Collections.Generic;
using System.Linq;





namespace CSP_Sudoku_BackTrack
{
    class Program
    {
        public static List<Variable> variables = new List<Variable>();
        public static List<Cage> cages = new List<Cage>();

        static void Main(string[] args)
        {
            Console.WriteLine("CSP Algorithm INITIALIZING BackTracking :) Sudoku Game (Killer Version)");
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
                if (index >= 25)
                {
                    for (int l = 0; l < 4; l++)
                    {
                        S = S.Remove(0, 1);
                        temp.I.Add(int.Parse(S.Substring(0, 1)) - 1);
                        S = S.Remove(0, 2);
                        temp.J.Add(int.Parse(S.Substring(0, 1)) - 1);
                        S = S.Remove(0, 3);
                    }
                    S = S.Remove(0, 1);
                    temp.I.Add(int.Parse(S.Substring(0, 1)) - 1);
                    S = S.Remove(0, 2);
                    temp.J.Add(int.Parse(S.Substring(0, 1)) - 1);
                    S = S.Remove(0, 2);
                    temp.Cage_Value = int.Parse(S);
                }
                else if (index >= 20)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        S = S.Remove(0, 1);
                        temp.I.Add(int.Parse(S.Substring(0, 1)) - 1);
                        S = S.Remove(0, 2);
                        temp.J.Add(int.Parse(S.Substring(0, 1)) - 1);
                        S = S.Remove(0, 3);
                    }
                    S = S.Remove(0, 1);
                    temp.I.Add(int.Parse(S.Substring(0, 1)) - 1);
                    S = S.Remove(0, 2);
                    temp.J.Add(int.Parse(S.Substring(0, 1)) - 1);
                    S = S.Remove(0, 2);
                    temp.Cage_Value = int.Parse(S);
                }
                else if(index>=16)
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

            for(int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                    Variable To_Add = new Variable();
                    To_Add.Domain = new List<int>();
                    To_Add.Value = 0;
                    To_Add.I = i;
                    To_Add.J = j;
                    for (int k = 1; k < 10; k++)
                    {
                        To_Add.Domain.Add(k);
                    }
                    variables.Add(To_Add);
                }
            }
      
            // Removing Unneccussary Values From Variables Domain And Initializing Variables ...
            foreach(Temp_Cage item in temp_Cages)
            {
                for(int number=0;number<item.I.Count;number++)
                {
                    if(item.I.Count == 2) // Execute Only If The Cage Has Two Value Inside ...
                    {
                        if (item.Cage_Value > 10)
                        {
                            for (int i = 1; i <= (item.Cage_Value - 10); i++)
                            {
                                variables.Where(x => x.I == item.I[number] && x.J == item.J[number]).First().Domain.Remove(i);
                            }
                        }
                        else
                        {
                            for (int i = item.Cage_Value; i < 10; i++)
                            {
                                variables.Where(x => x.I == item.I[number] && x.J == item.J[number]).First().Domain.Remove(i);
                            }
                        }
                    }
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
           
        }

        private static void BACKTRACKING_SEARCH()
        {
            RECURSIVE_BACKTRACKING();
        }

        private static bool RECURSIVE_BACKTRACKING()
        {
            if(AllHasValue(variables))
            {
                Print();
                return true;
            }
            Variable Un_Assigned = SELECT_UNASSIGNED_VARIABLE();
            foreach(int Value in Un_Assigned.Domain.ToList())
            {
                if (AllDiffer(Un_Assigned, Value))
                {
                    variables.Where(x => x.I == Un_Assigned.I && x.J == Un_Assigned.J).First().Value = Value;
                    variables.Where(x => x.I == Un_Assigned.I && x.J == Un_Assigned.J).First().Domain.Remove(Value);
                    variables.Where(x => x.I == Un_Assigned.I && x.J == Un_Assigned.J).First().Numbre_Of_Assingments += 1;
                    bool Result = RECURSIVE_BACKTRACKING();
                    if (Result)
                        return Result;
                    else
                    {
                        variables.Where(x => x.I == Un_Assigned.I && x.J == Un_Assigned.J).First().Domain.Add(Value);
                        variables.Where(x => x.I == Un_Assigned.I && x.J == Un_Assigned.J).First().Value = 0;
                    }
                }
            }
            return false;
        }

        private static Variable SELECT_UNASSIGNED_VARIABLE()
        {
            List<Variable> Sorted = DEEP_COPY();
            Sorted = Sorted.OrderBy(x => x.Domain.Count).ToList();
            int counter = 0;
            while(counter < Sorted.Count)
            {
                if(Sorted[counter].Value == 0)
                {
                    break;
                }
                counter++;
            }
            return Sorted[counter];
        }

        private static List<Variable> DEEP_COPY()
        {
            List<Variable> Copy = new List<Variable>();
            foreach(Variable item in variables)
            {
                Copy.Add(new Variable { Cage = item.Cage, Domain = item.Domain, I = item.I, J = item.J, Numbre_Of_Assingments = item.Numbre_Of_Assingments, Value = item.Value });
            }
            return Copy;
        }

        //private static bool Completed(List<int> vs)
        //{
        //    bool check = true;
        //    foreach(int item in vs)
        //    {
        //        if (item == 0)
        //        {
        //            check = false;
        //            break;
        //        }
        //    }
        //    return check;
        //}

        private static void Print()
        {
            Console.WriteLine("#################### Printing Board ##################");
            for(int i=0;i<9;i++)
            {
                Console.Write(" ");
                for(int j=0;j<9;j++)
                {
                    Console.Write(variables.Where(x => x.I == i && x.J == j).First().Value + " | ");
                }
                Console.WriteLine("\n-------------------------------------");
            }
            Console.WriteLine("\n@@@@@@@@@@@@@@@@@@@@@ Printing Number OF Assignments OF Each BOX @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\n");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write("[" + variables.Where(x => x.I == i && x.J == j).First().Numbre_Of_Assingments + "]" + " | ");
                    //Console.Write(variables.Where(x => x.I == i && x.J == j).First().Value + " | ");
                }
                Console.WriteLine("\n------------------------------------------------------------");
            }
            int Sum = 0;
            foreach (Variable item in variables)
            {
                Sum += item.Numbre_Of_Assingments;
            }
            Console.WriteLine("\n Total Number Of Assignments is {0}", Sum);
        }

        //private static bool Assign(int index)
        //{
        //     foreach (int item in variables[index].Domain)
        //     {
        //         bool ST = AllDiffer(index, item);
        //         if (ST)
        //         {
        //            //List<Variable> This_Variables_Version = new List<Variable>();
        //            //foreach (Variable item2 in variables)
        //            //{
        //            //    This_Variables_Version.Add(new Variable { Cage = item2.Cage, Domain = item2.Domain, I = item2.I, J = item2.J, Numbre_Of_Assingments = item2.Numbre_Of_Assingments, Value = item2.Value });
        //            //}
        //            //Variable[,] This_Board_Version = new Variable[9, 9];
        //            //for(int i=0;i<9;i++)
        //            //{
        //            //    for(int j=0;j<9;j++)
        //            //    {
        //            //        This_Board_Version[i, j] = new Variable { Cage = Board[i, j].Cage, Domain = Board[i, j].Domain, I = Board[i, j].I, J = Board[i, j].J, Numbre_Of_Assingments = Board[i, j].Numbre_Of_Assingments, Value = Board[i, j].Value };
        //            //    }
        //            //}
        //            //if (Forward_Checking(index, item))
        //            //{
        //                 variables[index].Value = item;
        //                 Board[variables[index].I, variables[index].J].Value = item;
        //                 variables[index].Numbre_Of_Assingments += 1;
        //                 Board[variables[index].I, variables[index].J].Numbre_Of_Assingments += 1;
        //                 //variables[index].Domain.Remove(item);
        //                 //Board[variables[index].I, variables[index].J].Domain.Remove(item);
        //                 if (AllHasValue(variables))
        //                 {
        //                     Print();
        //                     return true;
        //                 }
        //                 variables = variables.OrderBy(x => x.Domain.Count).ToList();
        //                 int empty_index = FindUnAssignedVariable(variables);
        //                 bool returnvalue = Assign(empty_index);
        //                 if(!returnvalue)
        //                 {
        //                    variables[index].Value = 0;
        //                    Board[variables[index].I, variables[index].J].Value = 0;
        //                    //variables = This_Variables_Version;
        //                    //Board = This_Board_Version;
        //                 }
        //            //}
        //            //else
        //            //{
        //            //    variables = This_Variables_Version;
        //            //    Board = This_Board_Version;
        //            //}
        //         }
        //     }
        //    return false;
        //}

        //private static bool Forward_Checking(int index, int item)
        //{
        //    List<Variable> SameSAT = new List<Variable>();
        //    List<Variable> SameSOT = new List<Variable>();
        //    int i = variables[index].I;
        //    int j = variables[index].J;
        //    for (int k = 0; k < 9; k++)
        //    {
        //        if(k != j)
        //            SameSAT.Add(Board[i, k]);
        //    }
        //    for (int w = 0; w < 9; w++)
        //    {
        //        if(w != i)
        //            SameSOT.Add(Board[w, j]);
        //    }
        //    int squere = Belongs_To(index);
        //    List<Variable> SameSquere = GetSameSquere(index, squere);
        //    if (SameSquere.Count != 8)
        //        throw new Exception("Should Be 8 :(");
        //    //bool First_Check = true;
        //    //for (int z = 0; z < SameSAT.Count; z++)
        //    //{
        //    //    if (Board[SameSAT[z].I, SameSAT[z].J].Domain.Count == 0 || Board[SameSOT[z].I, SameSOT[z].J].Domain.Count == 0 || Board[SameSquere[z].I, SameSquere[z].J].Domain.Count == 0)
        //    //    {
        //    //        First_Check = false;
        //    //        break;
        //    //    }
        //    //}
        //    //if(First_Check)
        //    //{
        //        for (int z = 0; z < SameSAT.Count; z++)
        //        {
        //            if (Board[SameSAT[z].I, SameSAT[z].J].Value == 0)
        //            {
        //                Board[SameSAT[z].I, SameSAT[z].J].Domain.Remove(item);
        //                variables.Where(x => x.I == SameSAT[z].I && x.J == SameSAT[z].J).First().Domain.Remove(item);
        //                if(Board[SameSAT[z].I, SameSAT[z].J].Domain.Count == 0)
        //                {
        //                    return false;
        //                }
        //            }
        //            if (Board[SameSOT[z].I, SameSOT[z].J].Value == 0)
        //            {
        //                Board[SameSOT[z].I, SameSOT[z].J].Domain.Remove(item);
        //                variables.Where(x => x.I == SameSOT[z].I && x.J == SameSOT[z].J).First().Domain.Remove(item);
        //                if (Board[SameSOT[z].I, SameSOT[z].J].Domain.Count == 0)
        //                {
        //                    return false;
        //                }
        //            }
        //            if (Board[SameSquere[z].I, SameSquere[z].J].Value == 0)
        //            {
        //                Board[SameSquere[z].I, SameSquere[z].J].Domain.Remove(item);
        //                variables.Where(x => x.I == SameSquere[z].I && x.J == SameSquere[z].J).First().Domain.Remove(item);
        //                if(Board[SameSquere[z].I, SameSquere[z].J].Domain.Count == 0)
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //    return true;
        //    //}
        //    //return First_Check;
        //}

        private static bool AllDiffer(Variable var, int value)
        {
            List<int> SameSAT = new List<int>();
            List<int> SameSOT = new List<int>();
            int i = var.I;
            int j = var.J;
            for (int k = 0; k < 9;k++)
            {
                if(k == j)
                {
                    continue;
                }
                else
                {
                    Variable Temp = variables.Where(x => x.I == i && x.J == k).First();
                    if (Temp.Value != 0)
                    {
                        SameSAT.Add(Temp.Value);
                    }
                }
            }
            for(int w = 0; w < 9;w++)
            {
                if (w == i)
                {
                    continue;
                }
                else
                {
                    Variable Temp = variables.Where(x => x.I == w && x.J == j).First();
                    if (Temp.Value != 0)
                    {
                        SameSOT.Add(Temp.Value);
                    }
                }
            }
            int squere = Belongs_To(var);
            List<int> SameSquere = GetSameSquere(var, squere);
            int Cage_Neighbors = 0;
            bool AllAssigned = true;
            if(var.Cage != null)
            {
                foreach (Variable item3 in var.Cage.variables)
                { 
                    if (item3.Value != 0)
                    {
                        Cage_Neighbors += item3.Value;
                    }
                    else
                    {
                        if(item3.I == var.I && item3.J == var.J)
                        {
                            continue;
                        }
                        else
                        {
                            AllAssigned = false;
                            break;
                        }
                    }
                }
            }
          
            if(var.Cage != null)
            {
                if (AllAssigned)
                {
                    if (SameSAT.Contains(value) || SameSOT.Contains(value) || SameSquere.Contains(value))
                    {
                        return false;
                    }
                    else
                    {
                        if (var.Cage.Cage_Value == (Cage_Neighbors + value))
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
                else
                {
                    if (SameSAT.Contains(value) || SameSOT.Contains(value) || SameSquere.Contains(value))
                    {
                        return false;
                    }
                    else
                        return true;
                }
            }
            else
            {
                if (SameSAT.Contains(value) || SameSOT.Contains(value) || SameSquere.Contains(value))
                {
                    return false;
                }
                else
                    return true;
            }
        }

        private static List<int> GetSameSquere(Variable var, int squere)
        {
            int I = var.I;
            int J = var.J;
            List<int> to_return = new List<int>();
            switch(squere)
            {
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (i == I && j == J)
                            {
                                continue;
                            }
                            else
                            {
                                Variable Temp = variables.Where(x => x.I == i && x.J == j).First();
                                //int index = variables.IndexOf(Temp);
                                if(Temp.Value != 0)
                                {
                                    to_return.Add(Temp.Value);
                                }
                            }
                        }
                    }
                    break;

                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (i == I && j == J)
                            {
                                continue;
                            }
                            else
                            {
                                Variable Temp = variables.Where(x => x.I == i && x.J == j).First();
                                //int index = variables.IndexOf(Temp);
                                if (Temp.Value != 0)
                                {
                                    to_return.Add(Temp.Value);
                                }
                            }
                        }
                    }
                    break;

                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (i == I && j == J)
                            {
                                continue;
                            }
                            else
                            {
                                Variable Temp = variables.Where(x => x.I == i && x.J == j).First();
                                //int index = variables.IndexOf(Temp);
                                if (Temp.Value != 0)
                                {
                                    to_return.Add(Temp.Value);
                                }
                            }
                        }
                    }
                    break;

                case 4:
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (i == I && j == J)
                            {
                                continue;
                            }
                            else
                            {
                                Variable Temp = variables.Where(x => x.I == i && x.J == j).First();
                                //int index = variables.IndexOf(Temp);
                                if (Temp.Value != 0)
                                {
                                    to_return.Add(Temp.Value);
                                }
                            }
                        }
                    }
                    break;

                case 5:
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (i == I && j == J)
                            {
                                continue;
                            }
                            else
                            {
                                Variable Temp = variables.Where(x => x.I == i && x.J == j).First();
                                //int index = variables.IndexOf(Temp);
                                if (Temp.Value != 0)
                                {
                                    to_return.Add(Temp.Value);
                                }
                            }
                        }
                    }
                    break;

                case 6:
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (i == I && j == J)
                            {
                                continue;
                            }
                            else
                            {
                                Variable Temp = variables.Where(x => x.I == i && x.J == j).First();
                                //int index = variables.IndexOf(Temp);
                                if (Temp.Value != 0)
                                {
                                    to_return.Add(Temp.Value);
                                }
                            }
                        }
                    }
                    break;

                case 7:
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (i == I && j == J)
                            {
                                continue;
                            }
                            else
                            {
                                Variable Temp = variables.Where(x => x.I == i && x.J == j).First();
                                //int index = variables.IndexOf(Temp);
                                if (Temp.Value != 0)
                                {
                                    to_return.Add(Temp.Value);
                                }
                            }
                        }
                    }
                    break;

                case 8:
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (i == I && j == J)
                            {
                                continue;
                            }
                            else
                            {
                                Variable Temp = variables.Where(x => x.I == i && x.J == j).First();
                                //int index = variables.IndexOf(Temp);
                                if (Temp.Value != 0)
                                {
                                    to_return.Add(Temp.Value);
                                }
                            }
                        }
                    }
                    break;

                case 9:
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (i == I && j == J)
                            {
                                continue;
                            }
                            else
                            {
                                Variable Temp = variables.Where(x => x.I == i && x.J == j).First();
                                //int index = variables.IndexOf(Temp);
                                if (Temp.Value != 0)
                                {
                                    to_return.Add(Temp.Value);
                                }
                            }
                        }
                    }
                    break;
            }
            return to_return;
        }

        private static int Belongs_To(Variable var)
        {
            int i = var.I;
            int j = var.J;
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

        //private static int FindUnAssignedVariable(List<Variable> variables)
        //{
        //    for(int i=0;i<variables.Count;i++)
        //    {
        //        if (variables[i].Value == 0)
        //            return i;
        //    }
        //    return -1;
        //}

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
