using Terraria;

namespace ChensCursedAccessories.Items
{
  public class ThornedChoker : ParentCursedAccessory
  {
    public static float dmgIncPercentage = 2f;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault("Reduces defense to 0\n" +
                         $"{ModHelpers.ToPercentage(dmgIncPercentage)}% of depleted defense is converted to melee damage\n" +
                         "The wearer will feel tremendous strength, but their fate will be under the command of something else.\n" +
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