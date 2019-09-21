using Terraria;
using Terraria.ID;

namespace ChensCursedAccessories.Items
{
  public class EyeOfVlad : ParentCursedAccessory
  {
    public static readonly int duration = ModHelpers.RoundOffToWhole(4f * 60);
    public static readonly float chance = 0.12f;
    public static readonly int[] buffsID = new int[] { BuffID.Bleeding, BuffID.Poisoned, BuffID.Venom };
    private readonly int regenDecrease = 1;
    private readonly int healthDecrease = 50;

    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault($"Attacking enemies has a {ModHelpers.ToPercentage(chance)}% chance to inflict Bleeding, Poisoned or Venom\n" +
                         $"Max Life decreased by {healthDecrease}\n" +
                         $"Slightly reduces health regeneration\n" +
                         "A hideous artifact required for reviving an ancient evil.\n" +
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
      player.lifeRegen -= regenDecrease;
      player.statLifeMax2 -= healthDecrease;
      player.GetModPlayer<AccessoryModPlayer>().eyeOfVlad = true;
    }
  }
}
