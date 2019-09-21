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
    public bool brokenShackles;
    public float brokenShacklesReducBonus;
    public float critDmgMultiplier;
    public bool daringThighGarter;
    public bool demonicHorns;
    public float demonicHornsCrit;
    public int demonicHornsTick;
    public bool earringOfDesire;
    public bool eyeOfVlad;
    public bool ringOfTemptation;
    public bool sashOfTheEvilOne;
    public float sashOfTheEvilOneCritDmg;
    public int sashOfTheEvilOneTickIncrement;
    public int sashOfTheEvilOneTickInBattle;
    public bool shadowCape;
    public bool theNail;
    public bool theNailHastened;
    public int theNailTick;
    public bool thornedChoker;
    public int thornedChokerDefBonus;

    public AccessoryModPlayer() => UpdateDead();

    // Overrides

    public override void ResetEffects()
    {
      beguilingNecklace = false;
      bleedingTooth = false;
      brokenShackles = false;
      brokenShacklesReducBonus = 0f;
      critDmgMultiplier = 1f;
      daringThighGarter = false;
      demonicHorns = false;
      earringOfDesire = false;
      eyeOfVlad = false;
      ringOfTemptation = false;
      sashOfTheEvilOne = false;
      shadowCape = false;
      theNail = false;
      thornedChoker = false;
      thornedChokerDefBonus = 0;
    }

    public override void UpdateDead()
    {
      ResetEffects();
      AssignDemonicHornsVariables();
      AssignSashOfTheEvilOneVariables();
      AssignTheNailVariables();
    }

    public override void PostUpdateEquips()
    {
      if (!demonicHorns) player.ClearBuff(mod.BuffType(DemonicHorns.buffType));
      if (beguilingNecklace) player.lifeRegen += ModHelpers.RoundOffToWhole(player.lifeRegen * BeguilingNecklace.regenMultiplier);
      if (bleedingTooth) player.statDefense -= ModHelpers.RoundOffToWhole(player.statDefense * BleedingTooth.statsMultiplier);
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
        critDmgMultiplier -= RingOfTemptation.critDmgReduction;
      }
      if (brokenShackles) brokenShacklesReducBonus = player.endurance;
      if (thornedChoker) thornedChokerDefBonus = player.statDefense;
    }

    public override void PostUpdateMiscEffects()
    {
      if (brokenShackles)
        player.endurance = 0;
      if (thornedChoker)
        player.statDefense = 0;
      if (demonicHorns)
        player.meleeCrit = 0;
      if (sashOfTheEvilOne)
        critDmgMultiplier = 0f;
    }

    public override void PostUpdateRunSpeeds()
    {
      if (earringOfDesire)
      {
        player.runAcceleration -= player.runAcceleration * EarringOfDesire.speedReduction;
        player.runSlowdown -= player.runSlowdown * EarringOfDesire.speedReduction;
      }
    }

    public override void PostUpdate()
    {
      if ((demonicHorns && demonicHornsTick++ >= DemonicHorns.tickReset) || !demonicHorns) AssignDemonicHornsVariables();
      if (sashOfTheEvilOne)
      {
        if (sashOfTheEvilOneTickInBattle < SashOfTheEvilOne.inBattleDuration)
        {
          sashOfTheEvilOneTickInBattle++;
          if (sashOfTheEvilOneTickIncrement++ >= SashOfTheEvilOne.tickInc)
          {
            sashOfTheEvilOneTickIncrement = 0;
            sashOfTheEvilOneCritDmg += SashOfTheEvilOne.incCritDmg;
          }
        }
        else sashOfTheEvilOneTickIncrement = 0;
      }
      else AssignSashOfTheEvilOneVariables();
      if (theNail && theNailHastened && (theNailTick++ >= TheNail.duration)) AssignTheNailVariables();
    }

    public override void GetWeaponCrit(Item item, ref int crit)
    {
      if (demonicHorns) crit += ModHelpers.RoundOffToWhole(demonicHornsCrit);
    }

    public override void GetWeaponKnockback(Item item, ref float knockback)
    {
      if (bleedingTooth) knockback -= (knockback * BleedingTooth.statsMultiplier);
    }

    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
      BleedingToothOnHit(damage);
      if (item.melee) DemonicHornsOnHit();
      EyeOfVladOnHit(ref target);
      TheNailOnHit();
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
      BleedingToothOnHit(damage);
      if (proj.melee) DemonicHornsOnHit();
      EyeOfVladOnHit(ref target);
      TheNailOnHit();
    }

    public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
    {
      EarringOfDesireModifyHitBy(ref damage);
      SashOfTheEvilOneModifyHitBy();
    }

    public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
    {
      EarringOfDesireModifyHitBy(ref damage);
      SashOfTheEvilOneModifyHitBy();
    }

    public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
    {
      if (item.melee)
      {
        if (brokenShackles) add += brokenShacklesReducBonus * BrokenShackles.dmgIncPercentage;
        if (thornedChoker) flat += thornedChokerDefBonus * ThornedChoker.dmgIncPercentage;
      }
    }

    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
      OverrideCriticalDamageModifyHit(ref damage, crit);
      if (item.melee) SashOfTheEvilOneModifyHit(ref damage, crit);
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
      OverrideCriticalDamageModifyHit(ref damage, crit);
      if (proj.melee) SashOfTheEvilOneModifyHit(ref damage, crit);
    }

    public override float UseTimeMultiplier(Item item)
    {
      if (theNail && IsAWeapon(item) && theNailHastened) return TheNail.useSpdMultiplier;

      return base.UseTimeMultiplier(item);
    }

    // Private Methods

    private void AssignDemonicHornsVariables()
    {
      demonicHornsCrit = 0f;
      demonicHornsTick = 0;
    }

    private void AssignSashOfTheEvilOneVariables()
    {
      sashOfTheEvilOneCritDmg = 0f;
      sashOfTheEvilOneTickInBattle = SashOfTheEvilOne.inBattleDuration;
      sashOfTheEvilOneTickIncrement = 0;
    }

    private void AssignTheNailVariables(bool reverse = false)
    {
      theNailHastened = reverse;
      theNailTick = 0;
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

    private void ComputeCriticalDamage(ref int dmg, int baseDmg)
    {
      dmg = baseDmg + ModHelpers.RoundOffToWhole(baseDmg * critDmgMultiplier);
    }

    private void DemonicHornsOnHit()
    {
      if (demonicHorns)
      {
        demonicHornsCrit += DemonicHorns.incCrit;
        demonicHornsTick = 0;
        // player.AddBuff(mod.BuffType(DemonicHorns.buffType), DemonicHorns.tickReset);
      }
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

    private void EyeOfVladOnHit(ref NPC victim)
    {
      if (eyeOfVlad && Main.rand.NextFloat() < EyeOfVlad.chance)
      {
        int randNum = Main.rand.Next(0, EyeOfVlad.buffsID.Length);
        victim.AddBuff(EyeOfVlad.buffsID[randNum], EyeOfVlad.duration);
      }
    }

    private int GetOriginalDamage(int outputDamage) => outputDamage / CritDmgBasis;

    private bool IsAWeapon(Item item) => item.melee || item.ranged || item.magic || item.thrown;

    private void LifeStealEffect()
    {
      for (int i = 0; i < 10; i++)
      {
        Dust.NewDust(player.Center, 1, 1, DustID.Blood); // Improve effects later
      }
    }

    private void OverrideCriticalDamageModifyHit(ref int dmg, bool crit)
    {
      if (ringOfTemptation && crit)
      {
        ComputeCriticalDamage(ref dmg, GetOriginalDamage(dmg));
      }
    }

    private void SashOfTheEvilOneModifyHit(ref int dmg, bool crit)
    {
      if (sashOfTheEvilOne)
      {
        if (crit)
        {
          critDmgMultiplier += sashOfTheEvilOneCritDmg;
          ComputeCriticalDamage(ref dmg, GetOriginalDamage(dmg));
        }
        sashOfTheEvilOneTickInBattle = 0;
        // player.AddBuff(mod.BuffType(SashOfTheEvilOne.buffType), SashOfTheEvilOne.inBattleDuration);
      }
    }

    private void SashOfTheEvilOneModifyHitBy()
    {
      if (sashOfTheEvilOne)
      {
        sashOfTheEvilOneCritDmg -= SashOfTheEvilOne.decCritDmg;
        sashOfTheEvilOneTickInBattle = 0;
        // player.AddBuff(mod.BuffType(SashOfTheEvilOne.buffType), SashOfTheEvilOne.inBattleDuration);
      }
    }

    private void TheNailOnHit()
    {
      if (theNail && Main.rand.NextFloat() < TheNail.chance) AssignTheNailVariables(true);
    }
  }
}
