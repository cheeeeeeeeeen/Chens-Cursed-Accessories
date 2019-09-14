using ChensCursedAccessories.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ChensCursedAccessories
{
  class AccessoryModPlayer : ModPlayer
  {
    private const int CritDmgBasis = 2;

    public bool beguilingNecklace;
    public bool bleedingTooth;
    public float critDmgMultiplier;
    public bool daringThighGarter;
    public bool earringOfDesire;
    public bool thornedChoker;
    public int thornedChokerDefBonus;
    public float thornedChokerReductBonus;
    public bool ringOfTemptation;

    public AccessoryModPlayer() => AssignVariables();

    public override void ResetEffects() => AssignVariables();

    private void AssignVariables()
    {
      beguilingNecklace = false;
      bleedingTooth = false;
      critDmgMultiplier = 1f;
      daringThighGarter = false;
      earringOfDesire = false;
      thornedChoker = false;
      thornedChokerDefBonus = 0;
      thornedChokerReductBonus = 0f;
      ringOfTemptation = false;
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
      if (ringOfTemptation)
      {
        float lifeLostPercentage = (player.statLifeMax2 - player.statLife) / (float)player.statLifeMax2;
        player.statDefense += ModHelpers.RoundOffToWhole(player.statDefense * lifeLostPercentage);
        player.endurance += player.endurance * (lifeLostPercentage / RingOfTemptation.percentageCapper);
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

    public override void PostUpdateRunSpeeds()
    {
      if (earringOfDesire)
      {
        player.runAcceleration -= player.runAcceleration * EarringOfDesire.speedReduction;
        player.runSlowdown -= player.runSlowdown * EarringOfDesire.speedReduction;
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

    public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
    {
      EarringOfDesireModifyHitBy(ref damage);
    }

    public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
    {
      EarringOfDesireModifyHitBy(ref damage);
    }

    public override void GetWeaponKnockback(Item item, ref float knockback)
    {
      if (bleedingTooth) knockback -= (knockback * BleedingTooth.statsMultiplier);
    }

    public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
    {
      if (thornedChoker && item.melee)
      {
        add += thornedChokerReductBonus * ThornedChoker.dmgIncPercentage;
        flat += thornedChokerDefBonus * ThornedChoker.dmgIncPercentage;
      }
    }

    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
      RingOfTemptationModifyHit(ref damage, ref crit);
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
      RingOfTemptationModifyHit(ref damage, ref crit);
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

    private void RingOfTemptationModifyHit(ref int dmg, ref bool crit)
    {
      if (ringOfTemptation && crit)
      {
        critDmgMultiplier -= RingOfTemptation.critDmgReduction;
        ComputeCriticalDamage(ref dmg, GetOriginalDamage(dmg));
      }
    }

    private int GetOriginalDamage(int outputDamage) => outputDamage / CritDmgBasis;

    private void ComputeCriticalDamage(ref int currentDmg, int baseDmg)
    {
      currentDmg = baseDmg + ModHelpers.RoundOffToWhole(baseDmg * critDmgMultiplier);
    }

    private void EarringOfDesireModifyHitBy(ref int dmg)
    {
      if (earringOfDesire && Main.rand.NextFloat() < EarringOfDesire.redirectChance)
      {
        int previousMana = player.statMana;
        player.statMana = Math.Max(0, player.statMana - dmg);
        dmg = player.statMana > 0 ? 0 : dmg - previousMana;
      }
    }
  }
}
