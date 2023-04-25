using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    public static class ListExtension
    {
        public static int Max<T>(this List<T> list,Type PropertyType)
        {
            Type entity = list.GetType();
            entity.GetProperty(PropertyType.Name);
            return 10;
        }
    }
}
