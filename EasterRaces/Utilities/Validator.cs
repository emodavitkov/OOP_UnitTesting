using System;

namespace EasterRaces.Utilities
{
    public static class Validator
    {
        public static void ThrowIfStringIsNullEmptyOrLessThan(string str,int minLength,string message)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length<minLength)
            {
                throw new ArgumentException(message);
            }
        }
    }
}