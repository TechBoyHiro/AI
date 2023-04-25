using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CARAStar
{
    public static class HashLookUpTable
    {
        private static List<string> HashTable = new List<string>();

        public static void AddHash(string hash)
        {
            //HashTable.Add(new StateHash { Creator = creator,Hash = hash});
            HashTable.Add(hash);
            Console.WriteLine(HashTable.IndexOf(hash).ToString() + "\t" + hash);
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
        public static bool IsInTable(string hash)
        {
            return HashTable.Contains(hash);
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

}
