using Terraria;

namespace ChensCursedAccessories.Items
{
  public class TheNail : ParentCursedAccessory
  {
    public static readonly int duration = ModHelpers.RoundOffToWhole(.3f * 60);
    public static readonly float chance = 0.05f;
    public static readonly float useSpdMultiplier = 2f;
    private readonly int critDecrease = 50;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault($"Attacking enemies has a {ModHelpers.ToPercentage(chance)}% chance to hasten attack speed\n" +
                         $"Critical rate is reduced by {critDecrease}%\n" +
                         "One of the relics known to summon an evil being.\n" +
                         "Artifact of the Vampire");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();

      // item.width = 32;
      // item.height = 32;
    }

    // public override string Texture => RealItemTexture;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      player.meleeCrit -= critDecrease;
      player.rangedCrit -= critDecrease;
      player.magicCrit -= critDecrease;
      player.thrownCrit -= critDecrease;
      player.GetModPlayer<AccessoryModPlayer>().theNail = true;
    }
  }
}
