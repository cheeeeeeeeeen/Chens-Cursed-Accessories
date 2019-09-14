using Terraria;

namespace ChensCursedAccessories.Items
{
  public class BrokenShackles : ParentCursedAccessory
  {
    public static float dmgIncPercentage = 1f;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault("Reduces damage reduction to 0%\n" +
                         $"{ModHelpers.ToPercentage(dmgIncPercentage)}% of depleted damage reduction is converted to bonus melee damage %\n" +
                         "Great power is within the shackles, and sacrificing sanity is the key to unleash it.\n" +
                         "Artifact of the Devil");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();

      // item.width = ;
      // item.height = ;
    }

    // public override string Texture => RealItemTexture;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.GetModPlayer<AccessoryModPlayer>().brokenShackles = true;
    }
  }
}