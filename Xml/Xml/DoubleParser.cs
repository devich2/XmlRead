using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Xml
{
    public class DoubleParser
    {
        public static double ParseDouble(object value)
        {
            double result;

            string doubleAsString = value.ToString();
            IEnumerable<char> doubleAsCharList = doubleAsString.ToList();

            if (doubleAsCharList.Where(ch => ch == '.' || ch == ',').Count() <= 1)
            {
                double.TryParse(doubleAsString.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out result);
            }
            else
            {
                if (doubleAsCharList.Where(ch => ch == '.').Count() <= 1
                    && doubleAsCharList.Where(ch => ch == ',').Count() > 1)
                {
                    double.TryParse(doubleAsString.Replace(",", string.Empty),
                        System.Globalization.NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out result);
                }
                else if (doubleAsCharList.Where(ch => ch == ',').Count() <= 1
                         && doubleAsCharList.Where(ch => ch == '.').Count() > 1)
                {
                    double.TryParse(doubleAsString.Replace(".", string.Empty).Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out result);
                }
                else
                {
                    throw new ArgumentException($"Error parsing {doubleAsString} as double, try removing thousand separators (if any)");
                }
            }

            return result;
        }
    }
}