using System;
using System.ComponentModel;

namespace FullStackCRM.Shared
{
    public static class EnumExtensions
    {
        #region Métodos Estáticos Públicos
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetEnumName(this Enum value)
        {
            return value.ToString();
        }

        public static int GetEnumValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }
        #endregion
    }
}
