using Terraria;

namespace ChensCursedAccessories.Items
{
  public abstract class ShadowCape : ParentCursedAccessory
  {
    public override void SetStaticDefaults()
    {
      Tooltip.SetDefault(
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
      
      player.GetModPlayer<AccessoryModPlayer>().shadowCape = true;
    }
  }
}
