using Terraria;

namespace ChensCursedAccessories.Items
{
  class RingOfTemptation : ParentCursedAccessory
  {
    private readonly int regenConstant = 1;
    public static readonly float percentageCapper = 10f;
    public static readonly float critDmgReduction = 0.75f;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault("Slightly increases health regeneration\n" +
                         "Lost health % is converted to defense and damage reduction\n" +
                         $"{ModHelpers.ToPercentage(critDmgReduction)}% decreased critical damage\n" +
                         "This ring is said to have enslaved the heart and mind of the beholder.\n" +
                         "Artifact of the Succubus");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();

      item.width = 30;
      item.height = 18;
    }

    public override string Texture => RealItemTexture;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.lifeRegen += regenConstant;
      player.GetModPlayer<AccessoryModPlayer>().ringOfTemptation = true;
    }
  }
}