using System.IO;
using Terraria.ModLoader;

namespace ChensCursedAccessories.Items
{
  public abstract class ParentCursedAccessory : ModItem
  {
    private const string placeHolderTexture = "ChensCursedAccessories/Items/placeholder";

    public string RealItemTexture => base.Texture;

    public override void SetDefaults()
    {
      item.accessory = true;
      item.width = 96;
      item.height = 77;
    }

    public override string Texture => placeHolderTexture;

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}
