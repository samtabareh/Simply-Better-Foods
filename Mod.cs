using HarmonyLib;
using UnityEngine;
using static SimplyBetterFoodsNS.FoodEffect;

namespace SimplyBetterFoodsNS
{
    public class SimplyBetterFoods : Mod
    {
        public static SimplyBetterFoods simply = new SimplyBetterFoods();
        public static WorldManager world = new WorldManager();

        internal static List<FoodToEffect> FoodToEffects = new List<FoodToEffect>();
        public static void AddFoodEffect(FoodToEffect FoodToEffect)
        {
            FoodToEffects.Add(FoodToEffect);
        }
        [HarmonyPatch(typeof(Food), "ConsumedBy")]
        [HarmonyPostfix]
        public static void Food__ConsumedBy_Postfix(Combatable vill, string ___Id)
        {
            Debug.LogWarning($"{simply.Manifest.Name} Consume Patch Applied.");
            for (int i = 0; i < FoodToEffects.Count; i++)
                if (___Id == FoodToEffects[i].Id)
                    switch (FoodToEffects[i].FoodEffects)
                    {
                        case FoodEffects.PermEffect:
                            FoodEffect.PermEffect();
                            break;
                        case FoodEffects.WellFed:
                            FoodEffect.WellFed(vill);
                            break;
                    }
        }

        private void Awake()
        {
            Harmony.PatchAll(typeof(SimplyBetterFoods));
            Harmony.PatchAll(typeof(FoodEffect));
        }
        public override void Ready()
        {
            AddFoodEffect(new FoodToEffect(FoodEffect.PermEffect(true), new FoodEffect(1), "apple"));
        }
    }
}