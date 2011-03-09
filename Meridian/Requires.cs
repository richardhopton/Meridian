using System;
using System.Globalization;

namespace Meridian
{
    public static class Requires
    {
        public static void NotNull(Object value, String parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void NotNullOrEmpty(String value, String parameterName)
        {
            NotNull(value, parameterName);

            if (value.Equals(String.Empty))
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, Strings.ArgumentException_EmptyString, parameterName));
            }
        }
    }
}
