using System;
using System.Collections.Generic;
using System.Text;

namespace SimplyBetterFoodsNS
{
    public enum FoodEffects
    {
        PermEffect, // Permanent Effect
        TempEffect, // Temporary Effect
        CBEffect, // Chance-Based Effect
        WellFed // Gives Well Fed Status Effect
    }

    public class FoodEffect
    {
        private static int Health;
        private static int Damage;
        private static int Defence;
        private static float AttackSpeed;
        private static float DebuffChance;
        private static float EffectTimer;

        public FoodEffect(int Health = 0, int Damage = 0, int Defence = 0, float AttackSpeed = 0, 
            float EffectChance = 0, float EffectTimer = 0)
        {
            FoodEffect.Health = Health;
            FoodEffect.Damage = Damage;
            FoodEffect.Defence = Defence;
            FoodEffect.AttackSpeed = AttackSpeed;
            FoodEffect.DebuffChance = EffectChance;
            FoodEffect.EffectTimer = EffectTimer;
        }
        public static void PermBuff(Combatable Vill)
        {
            Vill.BaseCombatStats.MaxHealth += Health;
            Vill.BaseCombatStats.AttackDamage += Damage;
            Vill.BaseCombatStats.Defence += Defence;
            Vill.BaseCombatStats.AttackSpeed += AttackSpeed;

        }
        public static void WellFed(Combatable Vill)
        {
            Vill.AddStatusEffect(new StatusEffect_WellFed());
            if (EffectTimer != 0)
                Vill.StatusEffects[Vill.StatusEffects.Count-1].FillAmount = -EffectTimer;
        }
    }
}