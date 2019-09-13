using Terraria;

namespace ChensCursedAccessories.Items
{
  public class BeguilingNecklace : ParentCursedAccessory
  {
    public static readonly float regenMultiplier = 3f;
    private readonly float damageMultiplier = .95f;
    private readonly int regenConstant = 0;

    public override void SetStaticDefaults() 
    {
      Tooltip.SetDefault($"{ModHelpers.ToPercentage(regenMultiplier)}% increased total health regeneration\n" +
                         $"{ModHelpers.ToPercentage(damageMultiplier)}% decreased damage\n" +
                         "The wearer will always feel great, but their energy is drained.\n" +
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