namespace ChensCursedAccessories.Buffs
{
  public class DevilsPower : ParentCursedBuff
  {
    public override void SetDefaults()
    {
      base.SetDefaults();

      DisplayName.SetDefault("Devil's Power");
      Description.SetDefault("Your hatred increases your critical rate.");
    }
  }
}