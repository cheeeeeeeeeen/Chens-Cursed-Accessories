using Terraria;

namespace ChensCursedAccessories.Items
{
  class EarringOfDesire : ParentCursedAccessory
  {
    private readonly int regenConstant = 1;
    public static readonly float redirectChance = 0.1f;
    public static readonly float speedReduction = 0.5f;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault("Slightly increases mana regeneration\n" +
                         $"Received damage has a {ModHelpers.ToPercentage(redirectChance)}% chance to be " +
                         "redirected to mana instead of health\n" +
                         $"{ModHelpers.ToPercentage(speedReduction)}% decreased running acceleration and deceleration\n" +
                         "This fine jewelry is known to protect the wearer in exchange for their inner energy.\n" +
                         "Artifact of the Succubus");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();

      item.width = 22;
      item.height = 40;
    }

    public override string Texture => RealItemTexture;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.manaRegen += regenConstant;
      player.GetModPlayer<AccessoryModPlayer>().earringOfDesire = true;
    }
  }
}