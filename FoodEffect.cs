using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SimplyBetterFoodsNS
{
    public enum FoodEffects
    {
        /// <summary>
        /// Only adds the Health Damage and Defence params.
        /// </summary>
        PermEffect, // Permanent Effect
        /// <summary>
        /// 
        /// </summary>
        TempEffect, // Temporary Effect
        /// <summary>
        /// 
        /// </summary>
        CBEffect, // Chance-Based Effect
        /// <summary>
        /// Only uses the EffectTimer param.
        /// </summary>
        WellFed // Gives Well Fed Status Effect
    }

    public class FoodEffect
    {
        internal static int Health;
        internal static int HealthAdded;
        internal static int Damage;
        internal static int Defence;
        internal static int AttackSpeed;
        internal static float DebuffChance;
        internal static float EffectTimer;
        /// <summary>
        /// </summary>
        /// <param name="Health">Max Value: 5</param>
        /// <param name="Damage">Max Value: 5</param>
        /// <param name="Defence">Max Value: 5</param>
        /// <param name="AttackSpeed">Max Value: 5</param>
        /// <param name="EffectChance">Min Value: 0.01f Max Value: 0.99f</param>
        public FoodEffect(int Health = 0, int Damage = 0, int Defence = 0, int AttackSpeed = 0, 
            float EffectChance = 0, float EffectTimer = 0)
        {
            FoodEffect.Health = Health;
            FoodEffect.Damage = Damage;
            FoodEffect.Defence = Defence;
            FoodEffect.AttackSpeed = AttackSpeed;
            FoodEffect.DebuffChance = EffectChance;
            FoodEffect.EffectTimer = EffectTimer;
        }
        private static bool PermEffectb = false;

        [HarmonyPatch(typeof(EndOfMonthCutscenes), "FeedVillagers")]
        private static void Prefix()
        {
            PermEffectb = true;
            Debug.LogWarning("Set bool true");
        }
        [HarmonyPatch(typeof(EndOfMonthCutscenes), "FeedVillagers")]
        private static void Postfix()
        {
            PermEffectb = true;
            Debug.LogWarning("Set bool false");
        }

        [HarmonyPatch(typeof(Combatable))]
        [HarmonyPatch(nameof(Combatable.ProcessedCombatStats), MethodType.Getter)]
        [HarmonyPostfix]
        private static void Combatable__ProcessedCombatStats_Postfix(ref CombatStats __result)
        {
            CombatStats stats = new CombatStats();
            if (Health + HealthAdded <= 5)
                stats.MaxHealth = Health;
            HealthAdded += Health;
            stats.AttackDamage = Damage;
            stats.Defence = Defence;
            stats.SpecialHits = new List<SpecialHit>();
            if (PermEffectb)
            {
                __result.AddStats(stats);
                Debug.LogWarning("Added stats to ProcessedCombatStats result.");
                Debug.LogWarning($"Health: {Health}");
                Debug.LogWarning($"Damage: {Damage}");
                Debug.LogWarning($"Defence: {Defence}");
                PermEffectb = false;
                Debug.LogWarning($"Set PermEffectb to {PermEffectb}");
            }
        }
        public static void WellFed(Combatable Vill)
        {
            Vill.AddStatusEffect(new StatusEffect_WellFed());
            if (EffectTimer != 0)
                Vill.StatusEffects[Vill.StatusEffects.Count-1].FillAmount = -EffectTimer;
        }
    }
}