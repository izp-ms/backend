using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers;
public static class DoubleExtensions
{
    public static double ToDouble(this string input)
    {
        CultureInfo[] cultures = new CultureInfo[]
        {
            CultureInfo.InvariantCulture,
            new CultureInfo("fr-FR"),
            new CultureInfo("de-DE"),
            new CultureInfo("pl-PL")
        };

        foreach (CultureInfo culture in cultures)
        {
            if (double.TryParse(input, NumberStyles.Float, culture, out double result))
            {
                return result;
            }
        }

        throw new FormatException("Input string is not in a valid numeric format.");
    }
}

