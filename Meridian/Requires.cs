using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Meridian
{
    internal static class Requires
    {
        internal static void NotNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        internal static void NotNullOrEmpty(string value, string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Equals(string.Empty))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ArgumentException_EmptyString, parameterName));
            }
        }
    }
}
