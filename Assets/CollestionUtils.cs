using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public static class NowickiUtils
    {

        private static Random rng = new Random();
        public static T PickRandom<T>(List<T> list)
        {
            return list[rng.Next(list.Count)];
        }
    }
}
