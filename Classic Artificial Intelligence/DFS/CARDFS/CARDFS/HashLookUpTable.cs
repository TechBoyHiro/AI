using System;
using System.Collections.Generic;
using System.Text;

namespace CARDFS
{
    public static class HashLookUpTable
    {
        private static List<string> HashTable = new List<string>();

        public static Winner AddHash(string hash)
        {
            bool win = false;
            int indexone = hash.IndexOf("1");
            HashTable.Add(hash);
            switch (indexone)
            {
                case 12:
                    if (hash.Substring(indexone + 2).StartsWith("0000"))
                        win = true;
                    break;
                case 13:
                    if (hash.Substring(indexone + 2).StartsWith("000"))
                        win = true;
                    break;
                case 14:
                    if (hash.Substring(indexone + 2).StartsWith("00"))
                        win = true;
                    break;
                case 15:
                    if (hash.Substring(indexone + 2).StartsWith("0"))
                        win = true;
                    break;
                case 16:
                    win = true;
                    break;
                default: break;
            }
            if (win)
            {
                Winner toreturn = new Winner();
                toreturn.hash = hash;
                toreturn.hashnumber = HashTable.IndexOf(hash) + 1;
                return toreturn;
            }
            else
                return null;
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
