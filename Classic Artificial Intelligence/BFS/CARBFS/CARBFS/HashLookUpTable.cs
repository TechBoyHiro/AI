using System;
using System.Collections.Generic;
using System.Text;

namespace CARBFS
{
    public static class HashLookUpTable
    {
        private static List<string> HashTable = new List<string>();

        public static void AddHash(string hash)
        {
            HashTable.Add(hash);
            Console.WriteLine("Hash : " + hash);
        }

        public static int FindHashIndex(string hash)
        {
            return (HashTable.IndexOf(hash) + 1);
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
