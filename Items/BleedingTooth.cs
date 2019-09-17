using Terraria;

namespace ChensCursedAccessories.Items
{
  public class BleedingTooth : ParentCursedAccessory
  {
    public static readonly float statsMultiplier = .5f;
    public static readonly float lifeStealMultiplier = .2f;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault($"{ModHelpers.ToPercentage(lifeStealMultiplier)}% life steal\n" +
                         $"{ModHelpers.ToPercentage(statsMultiplier)}% decreased damage, damage reduction, knockback,\n" +
                         "and critical rate for melee, ranged, magic and throwing damage\n" +
                         $"Total defense cut down by {ModHelpers.ToPercentage(statsMultiplier)}%\n" +
                         "It will grant the wearer the powers of a weakened night stalker.\n" +
                         "Artifact of the Vampire");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();

      item.width = 20;
      item.height = 30;
    }

    public override string Texture => RealItemTexture;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      int computedMultiplier = ModHelpers.RoundOffToWhole(ModHelpers.ToPercentage(statsMultiplier));

      player.allDamage -= statsMultiplier;
      player.endurance -= statsMultiplier;
      player.meleeCrit -= computedMultiplier;
      player.rangedCrit -= computedMultiplier;
      player.magicCrit -= computedMultiplier;
      player.thrownCrit -= computedMultiplier;

      player.GetModPlayer<AccessoryModPlayer>().bleedingTooth = true;
    }
  }
}