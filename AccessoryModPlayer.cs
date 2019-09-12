using ChensCursedAccessories.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ChensCursedAccessories
{
  class AccessoryModPlayer : ModPlayer
  {
    public bool beguilingNecklace = false;
    public bool bleedingTooth = false;
    public bool daringThighGarter = false;
    public bool thornedChoker = false;
    public int thornedChokerDefBonus = 0;
    public float thornedChokerReductBonus = 0f;

    public override void ResetEffects()
    {
      beguilingNecklace = false;
      bleedingTooth = false;
      daringThighGarter = false;
      thornedChoker = false;
      thornedChokerDefBonus = 0;
      thornedChokerReductBonus = 0f;
    }

    public override void PostUpdateEquips()
    {
      if (beguilingNecklace)
        player.lifeRegen += ModHelpers.RoundOffToWhole(player.lifeRegen * BeguilingNecklace.regenMultiplier);
      if (bleedingTooth)
        player.statDefense -= ModHelpers.RoundOffToWhole(player.statDefense * BleedingTooth.statsMultiplier);
      if (daringThighGarter)
      {
        player.statLifeMax2 += ModHelpers.RoundOffToWhole(player.statLifeMax2 * DaringThighGarter.lifeMultiplier);
        player.statManaMax2 -= ModHelpers.RoundOffToWhole(player.statManaMax2 * DaringThighGarter.manaMultiplier);
      }
      if (thornedChoker)
      {
        thornedChokerDefBonus = player.statDefense;
        thornedChokerReductBonus = player.endurance;
      }
    }

    public override void PostUpdateMiscEffects()
    {
      if (thornedChoker)
      {
        player.statDefense = 0;
        player.endurance = 0;
      }
    }

    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
      BleedingToothOnHit(damage);
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
      BleedingToothOnHit(damage);
    }

    public override void GetWeaponKnockback(Item item, ref float knockback)
    {
      if (bleedingTooth) knockback -= (knockback * BleedingTooth.statsMultiplier);
    }

    public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
    {
      if (thornedChoker)
      {
        add += thornedChokerReductBonus * ThornedChoker.dmgIncPercentage;
        flat += thornedChokerDefBonus * ThornedChoker.dmgIncPercentage;
      }
    }

    private void BleedingToothOnHit(int dmg)
    {
      if (bleedingTooth)
      {
        LifeStealEffect();
        int healValue = ModHelpers.RoundOffToWhole(dmg * BleedingTooth.lifeStealMultiplier);
        player.statLife += healValue;
        player.HealEffect(healValue, true);
      }
    }

    private void LifeStealEffect()
    {
      for (int i = 0; i < 10; i++)
      {
        Dust.NewDust(player.Center, 1, 1, DustID.Blood); // Improve effects later
      }
    }
  }
}
