using Terraria;

namespace ChensCursedAccessories.Items
{
  class RingOfTemptation : ParentCursedAccessory
  {
    private readonly int regenConstant = 1;
    public static readonly float critDmgReduction = 0.45f;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault("Slightly increases health regeneration\n" +
                         "Lost health % is converted to defense\n" +
                         $"{ModHelpers.ToPercentage(critDmgReduction)}% decreased critical damage\n" +
                         "This ring is said to have enslaved the minds of the beholder.\n" +
                         "Artifact of the Succubus");
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
      player.lifeRegen += regenConstant;
      player.meleeCrit = 100;
      player.GetModPlayer<AccessoryModPlayer>().ringOfTemptation = true;
    }
  }
}