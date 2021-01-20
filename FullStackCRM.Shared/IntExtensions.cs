using System;

namespace FullStackCRM.Shared
{
    public static class IntExtensions
    {
        public static T GetEnum<T>(this int n)
        {
            if (!Enum.IsDefined(typeof(T), n))
                throw new ArgumentOutOfRangeException(typeof(T).ToString(), "Valor de enum não encontrado");

            return (T)Enum.ToObject(typeof(T), n);
        }
    }
}
