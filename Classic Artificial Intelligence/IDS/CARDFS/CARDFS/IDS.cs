using System;
using System.Collections.Generic;
using System.Text;

namespace CARIDS
{
    public class IDS
    {
        public void Ids(CarNode root)
        {
            DFS dFS = new DFS();
            int L = 0;
            while (true)
            {
                if (L == 49)
                { 
                    int g = 0;
                }
                if(dFS.dfs(root,L))
                {
                    break;
                }
                HashLookUpTable.Reset();
                L++;
            }
        }
    }
}
