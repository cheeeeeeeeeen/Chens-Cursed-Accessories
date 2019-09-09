using System.IO;
using Terraria.ModLoader;

namespace ChensCursedAccessories.Items
{
  public abstract class ParentCursedAccessory : ModItem
  {
    private readonly string placeHolderPath = "ChensCursedAccessories/Items/placeholder";

    public override void SetDefaults()
    {
      item.accessory = true;
      item.width = 96;
      item.height = 77;
    }

    public override string Texture => File.Exists(base.Texture) ? base.Texture : placeHolderPath;

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.SetResult(this);
      recipe.AddRecipe();
    }
  }
}
