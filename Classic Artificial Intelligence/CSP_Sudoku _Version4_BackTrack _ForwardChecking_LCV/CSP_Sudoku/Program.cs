using System;
using System.Collections.Generic;
using System.Linq;


namespace CSP_Sudoku_BackTrack_ForwardChecking_LCV
{
    class Program
    {
        //public static Variable[,] Board = new Variable[9, 9];
        public static List<Variable> variables = new List<Variable>();
        public static List<Cage> cages = new List<Cage>();

        static void Main(string[] args)
        {
            Console.WriteLine("CSP Algorithm BackTracking + ForwardChecking + LCV INITIALIZING :) Sudoku Game (Killer Version)");
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
                if(index >= 25)
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
                else if(index >= 20)
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
            LCV(Un_Assigned);
            foreach(int Value in Un_Assigned.Domain.ToList())
            {
                if (AllDiffer(Un_Assigned, Value))
                {
                    Forward_Checking_Respond Temp = new Forward_Checking_Respond();
                    Temp = Forward_Checking(Un_Assigned, Value);
                    if(Temp.Result)
                    {
                        variables.Where(x => x.I == Un_Assigned.I && x.J == Un_Assigned.J).First().Value = Value;
                        variables.Where(x => x.I == Un_Assigned.I && x.J == Un_Assigned.J).First().Numbre_Of_Assingments += 1;
                        bool Result = RECURSIVE_BACKTRACKING();
                        if (Result)
                            return Result;
                        else
                        {
                            variables.Where(x => x.I == Un_Assigned.I && x.J == Un_Assigned.J).First().Value = 0;
                            Undo_ForwardChecking(Temp.Deleted_From, Value);
                        }
                    }
                }
            }
            return false;
        }

        private static void LCV(Variable v)
        {
            int[] MINIMUM_LCV = new int[v.Domain.Count];
            int assigned = 0;
            for(int q=0;q<v.Domain.Count;q++)
            {
                List<Variable> SameSAT = new List<Variable>();
                List<Variable> SameSOT = new List<Variable>();
                int Temp = 0;
                int i = v.I;
                int j = v.J;
                for (int k = 0; k < 9; k++)
                {
                    if (k == j)
                    {
                        continue;
                    }
                    else
                    {
                        SameSAT.Add(variables.Where(x => x.I == i && x.J == k).First());
                    }
                }
                for (int w = 0; w < 9; w++)
                {
                    if (w == i)
                    {
                        continue;
                    }
                    else
                    {
                        SameSOT.Add(variables.Where(x => x.I == w && x.J == j).First());
                    }
                }
                int squere = Belongs_To(v);
                List<Variable> SameSquere = GetSameSquere(v, squere);
                if (SameSquere.Count != 8)
                    throw new Exception("Should Be 8 :(");
                bool CH_SAT = CheckDomain(SameSAT, v.Domain[q]);
                if (!CH_SAT)
                {
                    MINIMUM_LCV[assigned] = v.Domain[q];
                    assigned++;
                    continue;
                }
                bool CH_SOT = CheckDomain(SameSOT, v.Domain[q]);
                if (!CH_SOT)
                {
                    MINIMUM_LCV[assigned] = v.Domain[q];
                    assigned++;
                    continue;
                }
                bool CH_SQUERE = CheckDomain(SameSquere, v.Domain[q]);
                if (!CH_SQUERE)
                {
                    MINIMUM_LCV[assigned] = v.Domain[q];
                    assigned++;
                    continue;   
                }
                for (int z = 0; z < SameSAT.Count; z++)
                {
                    if (variables.Where(x => x.I == SameSAT[z].I && x.J == SameSAT[z].J).First().Value == 0)
                    {
                        Temp++;
                    }

                    if (variables.Where(x => x.I == SameSOT[z].I && x.J == SameSOT[z].J).First().Value == 0)
                    {
                        Temp++;
                    }

                    if (variables.Where(x => x.I == SameSquere[z].I && x.J == SameSquere[z].J).First().Value == 0)
                    {
                        Temp++;
                    }
                }
                int r = 0;
                while(r<assigned)
                {
                    if(Temp < MINIMUM_LCV[r])
                    {
                        MINIMUM_LCV[assigned] = MINIMUM_LCV[r];
                        MINIMUM_LCV[r] = v.Domain[q];
                        assigned++;
                        break;
                    }
                    r++;
                }
                if (r >= assigned)
                {
                    MINIMUM_LCV[assigned] = v.Domain[q];
                    assigned++;
                }
            }
            variables.Where(x => x.I == v.I && x.J == v.J).First().Domain = MINIMUM_LCV.ToList();
            v.Domain = MINIMUM_LCV.ToList();
        }

        private static void Undo_ForwardChecking(List<Variable> list, int value)
        {
            foreach(Variable item in list)
            {
                variables.Where(x => x.I == item.I && x.J == item.J).First().Domain.Add(value);
            }
            //List<Variable> SameSAT = new List<Variable>();
            //List<Variable> SameSOT = new List<Variable>();
            //int i = var.I;
            //int j = var.J;
            //for (int k = 0; k < 9; k++)
            //{
            //    if (k == j)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        SameSAT.Add(variables.Where(x => x.I == i && x.J == k).First());
            //    }
            //}
            //for (int w = 0; w < 9; w++)
            //{
            //    if (w == i)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        SameSOT.Add(variables.Where(x => x.I == w && x.J == j).First());
            //    }
            //}
            //int squere = Belongs_To(var);
            //List<Variable> SameSquere = GetSameSquere(var, squere);
            //if (SameSquere.Count != 8)
            //    throw new Exception("Should Be 8 :(");
            //for (int z = 0; z < SameSAT.Count; z++)
            //{
            //    //if (variables.Where(x => x.I == SameSAT[z].I && x.J == SameSAT[z].J).First().Value == 0)
            //    {
            //        variables.Where(x => x.I == SameSAT[z].I && x.J == SameSAT[z].J).First().Domain.Add(value);
            //    }

            //    //if (variables.Where(x => x.I == SameSOT[z].I && x.J == SameSOT[z].J).First().Value == 0)
            //    {
            //        variables.Where(x => x.I == SameSOT[z].I && x.J == SameSOT[z].J).First().Domain.Add(value);
            //    }

            //    //if (variables.Where(x => x.I == SameSquere[z].I && x.J == SameSquere[z].J).First().Value == 0)
            //    {
            //        variables.Where(x => x.I == SameSquere[z].I && x.J == SameSquere[z].J).First().Domain.Add(value);
            //    }
            //}
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

        private static bool Completed(List<int> vs)
        {
            bool check = true;
            foreach(int item in vs)
            {
                if (item == 0)
                {
                    check = false;
                    break;
                }
            }
            return check;
        }

        private static void Print()
        {
            Console.WriteLine("#################### Printing Board ##################");
            for (int i = 0; i < 9; i++)
            {
                Console.Write(" ");
                for (int j = 0; j < 9; j++)
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
            foreach(Variable item in variables)
            {
                Sum += item.Numbre_Of_Assingments;
            }
            Console.WriteLine("\n Total Number Of Assignments is {0}", Sum);
        }

        private static Forward_Checking_Respond Forward_Checking(Variable var, int value)
        {
            List<Variable> SameSAT = new List<Variable>();
            List<Variable> SameSOT = new List<Variable>();
            Forward_Checking_Respond _Respond = new Forward_Checking_Respond();
            int i = var.I;
            int j = var.J;
            for (int k = 0; k < 9; k++)
            {
                if (k == j)
                {
                    continue;
                }
                else
                {
                    SameSAT.Add(variables.Where(x => x.I == i && x.J == k).First());
                }
            }
            for (int w = 0; w < 9; w++)
            {
                if (w == i)
                {
                    continue;
                }
                else
                {
                    SameSOT.Add(variables.Where(x => x.I == w && x.J == j).First());
                }
            }
            int squere = Belongs_To(var);
            List<Variable> SameSquere = GetSameSquere(var, squere);
            if (SameSquere.Count != 8)
                throw new Exception("Should Be 8 :(");
            bool CH_SAT = CheckDomain(SameSAT,value);
            if(!CH_SAT)
            {
                _Respond.Result = false;
                return _Respond;
            }
            bool CH_SOT = CheckDomain(SameSOT, value);
            if(!CH_SOT)
            {
                _Respond.Result = false;
                return _Respond;
            }
            bool CH_SQUERE = CheckDomain(SameSquere, value);
            if(!CH_SQUERE)
            {
                _Respond.Result = false;
                return _Respond;
            }
            
            for (int z = 0; z < SameSAT.Count; z++)
            {
                if (variables.Where(x => x.I == SameSAT[z].I && x.J == SameSAT[z].J).First().Value == 0)
                {
                    bool removed = variables.Where(x => x.I == SameSAT[z].I && x.J == SameSAT[z].J).First().Domain.Remove(value);
                    if(removed)
                    {
                        _Respond.Deleted_From.Add(variables.Where(x => x.I == SameSAT[z].I && x.J == SameSAT[z].J).First());
                    }
                }

                if (variables.Where(x => x.I == SameSOT[z].I && x.J == SameSOT[z].J).First().Value == 0)
                {
                    bool removed = variables.Where(x => x.I == SameSOT[z].I && x.J == SameSOT[z].J).First().Domain.Remove(value);
                    if(removed)
                    {
                        _Respond.Deleted_From.Add(variables.Where(x => x.I == SameSOT[z].I && x.J == SameSOT[z].J).First());
                    }
                }

                if (variables.Where(x => x.I == SameSquere[z].I && x.J == SameSquere[z].J).First().Value == 0)
                {
                    bool removed = variables.Where(x => x.I == SameSquere[z].I && x.J == SameSquere[z].J).First().Domain.Remove(value);
                    if(removed)
                    {
                        _Respond.Deleted_From.Add(variables.Where(x => x.I == SameSquere[z].I && x.J == SameSquere[z].J).First());
                    }
                }
            }
            _Respond.Result = true;
            return _Respond;
        }

        private static bool CheckDomain(List<Variable> list, int value)
        {
            bool Check = true;
            foreach(Variable item in list)
            {
                if(item.Domain.Contains(value))
                {
                    item.Domain.Remove(value);
                    if(item.Domain.Count == 0)
                    {
                        item.Domain.Add(value);
                        Check = false;
                        break;
                    }
                    item.Domain.Add(value);
                }
            }
            return Check;
        }

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
                    //int index = variables.IndexOf(Temp);
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
                    //int index = variables.IndexOf(Temp);
                    if (Temp.Value != 0)
                    {
                        SameSOT.Add(Temp.Value);
                    }
                }
            }
            int squere = Belongs_To(var);
            List<Variable> Extraction = GetSameSquere(var, squere);
            List<int> SameSquere = new List<int>();
            foreach (Variable item2 in Extraction)
            {
                if (item2.Value != 0)
                    SameSquere.Add(item2.Value);
            }
            int Cage_Neighbors = 0;
            bool AllAssigned = true;
            if(var.Cage != null)
            {
                foreach (Variable item3 in var.Cage.variables)
                {
                    //Variable Temp = variables.Where(x => x.I == item3.I && x.J == item3.J).First();
                    //int index = variables.IndexOf(Temp);
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

        private static List<Variable> GetSameSquere(Variable var, int squere)
        {
            int I = var.I;
            int J = var.J;
            List<Variable> to_return = new List<Variable>();
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
                                to_return.Add(variables.Where(x => x.I == i && x.J == j).First());
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
                                to_return.Add(variables.Where(x => x.I == i && x.J == j).First());
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
                                to_return.Add(variables.Where(x => x.I == i && x.J == j).First());
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
                                to_return.Add(variables.Where(x => x.I == i && x.J == j).First());
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
                                to_return.Add(variables.Where(x => x.I == i && x.J == j).First());
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
                                to_return.Add(variables.Where(x => x.I == i && x.J == j).First());
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
                                to_return.Add(variables.Where(x => x.I == i && x.J == j).First());
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
                                to_return.Add(variables.Where(x => x.I == i && x.J == j).First());
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
                                to_return.Add(variables.Where(x => x.I == i && x.J == j).First());
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
