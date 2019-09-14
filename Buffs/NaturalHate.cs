namespace ChensCursedAccessories.Buffs
{
  public class NaturalHate : ParentCursedBuff
  {
    public override void SetDefaults()
    {
      base.SetDefaults();

      Description.SetDefault("Your hatred increases your critical rate.");
    }
  }
}