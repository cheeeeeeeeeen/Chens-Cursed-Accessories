using Terraria;

namespace ChensCursedAccessories.Items
{
  public class BleedingTooth : ParentCursedAccessory
  {
    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault("It will grant the wearer the powers of a weakened vampire.");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();

      // item.width;
      // item.height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.GetModPlayer<AccessoryModPlayer>().bleedingTooth = true;
    }
  }
}