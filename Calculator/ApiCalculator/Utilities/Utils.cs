namespace ApiCalculator.Utilities;

public static class Utils
{
    public static float TryParseToFloat(string number)
    {
        if (float.TryParse(number, out float result))
        {
            return result;
        }
        throw new ArgumentException("Provided input doesn't appear to be a number");
    }
}