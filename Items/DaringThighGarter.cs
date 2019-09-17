using Terraria;

namespace ChensCursedAccessories.Items
{
  class DaringThighGarter : ParentCursedAccessory
  {
    public static readonly float lifeMultiplier = 0.5f;
    public static readonly float manaMultiplier = 0.75f;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault($"{ModHelpers.ToPercentage(lifeMultiplier)}% increased total health\n" +
                         $"{ModHelpers.ToPercentage(manaMultiplier)}% decreased total mana\n" +
                         "The wearer will feel proud of their physical build, eventually losing their thoughts completely.\n" +
                         "Artifact of the Succubus");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();

      item.width = 34;
      item.height = 38;
    }

    public override string Texture => RealItemTexture;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.GetModPlayer<AccessoryModPlayer>().daringThighGarter = true;
    }
  }
}
