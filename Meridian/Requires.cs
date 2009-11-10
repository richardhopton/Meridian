using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Meridian
{
    public static class Requires
    {
        public static void NotNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void NotNullOrEmpty(string value, string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Equals(string.Empty))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ArgumentException_EmptyString, parameterName));
            }
        }
    }
}
