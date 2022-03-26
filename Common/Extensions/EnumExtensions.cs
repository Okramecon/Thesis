using System;
using System.ComponentModel;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string Description<T>(this T val)
        {
            var defaultDescription = val.ToString();
            var fieldInfo = val.GetType().GetMember(defaultDescription);
            if (fieldInfo.Length > 0)
            {
                var attributes = fieldInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return defaultDescription;
        }
    }
}
