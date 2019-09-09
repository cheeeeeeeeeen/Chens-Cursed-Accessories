using Terraria;

namespace ChensCursedAccessories.Items
{
  public class BeguilingNecklace : ParentCursedAccessory
  {
    private const float damageMultiplier = .95f;
    private const int regenConstant = 0;

    public override void SetStaticDefaults() 
    {
      Tooltip.SetDefault("The wearer will always feel great, but saps away their energy.");
    }

    public override void SetDefaults() 
    {
      base.SetDefaults();

      // item.width;
      // item.height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.lifeRegen += regenConstant;
      player.allDamage -= damageMultiplier;

      player.GetModPlayer<AccessoryModPlayer>().beguilingNecklace = true;
    }
  }
}