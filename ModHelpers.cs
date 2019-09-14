using System;

namespace ChensCursedAccessories
{
  internal static class ModHelpers
  {
    public static int RoundOffToWhole(float num)
    {
      string numStr = num.ToString();
      int decimalLength = numStr.Substring(numStr.IndexOf(".") + 1).Length;
      return (int)Math.Round(num, decimalLength, MidpointRounding.AwayFromZero);
    }

    public static float ToPercentage(float num) => num * 100;

    public static float ToSeconds(int ticks) => ticks / 60f;

    public static string PluralizeSecond(int ticks)
    {
      if (ticks == 60)
        return "second";

      return "seconds";
    }
  }
}
