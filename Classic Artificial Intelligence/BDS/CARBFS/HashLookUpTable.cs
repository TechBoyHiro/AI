using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CARBDS
{
    public static class HashLookUpTable
    {
        private static List<string> HashTable = new List<string>();
        private static List<int> Creators = new List<int>();

        public static void AddHash(string hash,int creator)
        {
            //HashTable.Add(new StateHash { Creator = creator,Hash = hash});
            HashTable.Add(hash);
            Creators.Add(creator);
            Console.WriteLine(HashTable.IndexOf(hash).ToString() + "\t" + hash + "\t" + "created by: " + "\t" + creator);
        }

        public static int FindHashIndex(string hash )
        {
            //return (HashTable.IndexOf(new StateHash { Hash = hash , Creator = creator } ) + 1);
            return HashTable.IndexOf(hash)+1;
        }

        /// <summary>
        /// check if hash is already in the list or not true if it was in table
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool IsInTable(string hash,int creator)
        {
            //return HashTable.Contains(new StateHash { Hash = hash,Creator = creator});
            if(HashTable.Contains(hash))
            {
                if (creator == Creators[HashTable.IndexOf(hash)])
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        /// <summary>
        /// returns the number of hash's in the table
        /// </summary>
        /// <returns></returns>
        public static int HashCounter()
        {
            return HashTable.Count;
        }
    }

    public class StateHash
    {
        public string Hash { get; set; }
        public Creator Creator { get; set; }
    }

    public enum Creator
    {
        Root =1 ,
        Goal = 2
    }
}
