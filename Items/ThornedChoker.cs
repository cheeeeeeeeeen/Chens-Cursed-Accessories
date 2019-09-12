using Terraria;

namespace ChensCursedAccessories.Items
{
  public class ThornedChoker : ParentCursedAccessory
  {
    public static float dmgIncPercentage = 1.5f;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault("Reduces defense and damage reduction to 0\n" +
                         $"{ModHelpers.ToPercentage(dmgIncPercentage)}% of depleted defense is converted to damage\n" +
                         "The wearer will feel tremendous power, but their fate is now under the command of something else.\n" +
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
      player.GetModPlayer<AccessoryModPlayer>().thornedChoker = true;
    }
  }
}