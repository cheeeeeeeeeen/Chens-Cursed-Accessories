using System;
using Terraria.ModLoader;

namespace ChensCursedAccessories
{
  class AccessoryModPlayer : ModPlayer
  {
    private const float regenMultiplier = 4f;
    public bool beguilingNecklace = false;

    public override void ResetEffects()
    {
      beguilingNecklace = false;
    }

    public override void PostUpdateEquips()
    {
      if (beguilingNecklace)
      {
        player.lifeRegen = RoundOffToWhole(player.lifeRegen * regenMultiplier);
      }
    }

    private int RoundOffToWhole(float num)
    {
      string numStr = num.ToString();
      int decimalLength = numStr.Substring(numStr.IndexOf(".") + 1).Length;
      return (int)Math.Round(num, decimalLength, MidpointRounding.AwayFromZero);
    }
  }
}
