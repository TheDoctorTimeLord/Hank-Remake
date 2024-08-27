using System;

namespace Core.Utils
{
    public static class IdUtils
    {
        private static readonly Random Random = new Random();
        
        public static string GenerateIdStr<T>()
        {
            return typeof(T).FullName + Random.Next(0, int.MaxValue);
        }

        public static int GenerateIdInt<T>()
        {
            return Random.Next();
        }
    }
}