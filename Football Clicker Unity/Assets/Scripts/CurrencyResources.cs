namespace Assets.Scripts
{
    public static class CurrencyResources
    {
        public static string CurrencyToString(float valueToConvert)
        {
            return CurrencyToString(valueToConvert, false);
        }

        public static string CurrencyToString(float valueToConvert, bool trimLargeNumberString)
        {
            int enumValue = -1;
            double v = (double)valueToConvert;
            while (v >= 1000d)
            {
                v /= 1000d;
                enumValue++;
            }

            if (enumValue >= 0)
            {
                if (trimLargeNumberString)
                {
                    return v.ToString("£0.###") + " " + ((LargeNumberValue)enumValue).ToString().Substring(0, 3);
                }
                return v.ToString("£0.###") + " " + (LargeNumberValue)enumValue;
            }

            return v.ToString("£0.###");
        }
    }


}


