using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ChensCursedAccessories.Items
{
  public class SashOfTheEvilOne : ParentCursedAccessory
  {
    public static readonly float incCritDmg = 0.01f;
    public static readonly float decCritDmg = 0.02f;
    public static readonly int tickInc = 1 * 60;
    public static readonly int inBattleDuration = 5 * 60;
    public static readonly string buffType = "DevilsPower";

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault("Reduces melee critical damage to 0%\n" +
                         $"Critical damage will increase by {ModHelpers.ToPercentage(incCritDmg)}% every " +
                         $"{ModHelpers.ToSeconds(tickInc)} {ModHelpers.PluralizeSecond(tickInc)} when in battle\n" +
                         $"Critical damage will decrease by {ModHelpers.ToPercentage(decCritDmg)}% if hurt\n" +
                         "The very garments of the evil one.\n" +
                         "The wearer will have the same power as the evil one, but at what cost?\n" +
                         "Artifact of the Devil");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();

      item.width = 32;
      item.height = 28;
    }

    public override string Texture => RealItemTexture;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      ItemOwner(player).sashOfTheEvilOne = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
      AccessoryModPlayer modPlayer = ItemOwner(Main.player[item.owner]);
      if (modPlayer.sashOfTheEvilOne)
      {
        TooltipLine newLine = new TooltipLine(mod, "Tracker", $"Current Critical Damage: {ModHelpers.ToPercentage(modPlayer.sashOfTheEvilOneCritDmg)}%");
        tooltips.Add(newLine);
      }
    }

    private AccessoryModPlayer ItemOwner(Player player) => player.GetModPlayer<AccessoryModPlayer>();
  }
}
