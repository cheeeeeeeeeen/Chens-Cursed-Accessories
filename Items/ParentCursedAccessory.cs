using Terraria.ModLoader;

namespace ChensCursedAccessories.Items
{
  public abstract class ParentCursedAccessory : ModItem
  {
    private readonly string placeHolderTexture = "ChensCursedAccessories/Items/placeholder";

    public string RealItemTexture => base.Texture;

    public override void SetDefaults()
    {
      item.accessory = true;
      item.width = 96;
      item.height = 77;
      item.rare = 2;
    }

    public override string Texture => placeHolderTexture;
  }
}
