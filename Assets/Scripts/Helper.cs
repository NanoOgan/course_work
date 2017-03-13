using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Assets
{
    public static class Helper
    {
        public static T GetRandomElement<T>(this List<T> list)
        {

            if (list.Count != 0)
            {
                int index = RND(list.Count - 1);
                return list[index];
            }
            else
                throw new Exception(list.ToString());
        }

        private static int RND(int max)
        {
            Random random = new Random();
            int index = random.Next(0, max * 100000);
            return index / 100000;
        }

        public static void Shuffle<T>(this List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
