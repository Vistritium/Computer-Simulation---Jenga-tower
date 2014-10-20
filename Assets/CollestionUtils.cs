using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public static class NowickiUtils
    {

        private static Random rnd = new Random();
        public static T Random<T>(this List<T> list)
        {
            return list[rnd.Next(list.Count)];
        }
    }
}
