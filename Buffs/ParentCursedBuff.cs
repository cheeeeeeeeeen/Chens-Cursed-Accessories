using System.Text.RegularExpressions;
using Terraria;
using Terraria.ModLoader;

namespace ChensCursedAccessories.Buffs
{
  public abstract class ParentCursedBuff : ModBuff
  {
    public override void SetDefaults()
    {
      DisplayName.SetDefault(BuffName);
      Description.SetDefault($"{BuffName} is not overriden");
      Main.debuff[Type] = true;
      Main.pvpBuff[Type] = true;
      Main.buffNoSave[Type] = true;
      canBeCleared = false;
    }

    private string BuffName => Regex.Replace(Name, "([A-Z])", " $1").Trim();
  }
}
