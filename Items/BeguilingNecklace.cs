using Terraria;
using Terraria.ModLoader;

namespace ChensCursedAccessories.Items
{
  [AutoloadEquip(EquipType.Front)]

  public class BeguilingNecklace : ParentCursedAccessory
  {
    private const float damageMultiplier = .95f;
    private const int regenConstant = 0;

    public override void SetStaticDefaults() 
    {
      Tooltip.SetDefault("The wearer will always feel great, but saps away their energy.\n" +
                         "Artifact of the Succubus");
    }

    public override void SetDefaults() 
    {
      base.SetDefaults();

      item.width = 32;
      item.height = 32;
    }

    public override string Texture => RealItemTexture;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.lifeRegen += regenConstant;
      player.allDamage -= damageMultiplier;

      player.GetModPlayer<AccessoryModPlayer>().beguilingNecklace = true;
    }
  }
}