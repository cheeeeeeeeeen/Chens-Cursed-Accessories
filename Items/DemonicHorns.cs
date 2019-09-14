using Terraria;

namespace ChensCursedAccessories.Items
{
  class DemonicHorns : ParentCursedAccessory
  {
    public static readonly float incCrit = 1f;
    public static readonly int tickReset = 5 * 60;
    public static readonly string buffType = "NaturalHate";

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault("Reduces melee critical rate to 0%\n" +
                         $"Critical rate will increase by {incCrit}% for every successful hit with a melee weapon\n" +
                         $"Critical rate will reset back to 0% after not landing a hit for " +
                         $"{ModHelpers.ToSeconds(tickReset)} {ModHelpers.PluralizeSecond(tickReset)}\n" +
                         "The horns themselves come from the devil itself.\n" +
                         "Natural hate over everything will be built upon the owner's heart." +
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
      player.meleeCrit = 0;
      player.GetModPlayer<AccessoryModPlayer>().demonicHorns = true;
    }
  }
}
