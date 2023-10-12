using System.Globalization;

namespace Application.Helpers;
public static class DoubleExtensions
{
    public static double ToDouble(this string input)
    {
        CultureInfo[] cultures = new CultureInfo[]
        {
            CultureInfo.InvariantCulture,
            new CultureInfo("en-EN"),
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

