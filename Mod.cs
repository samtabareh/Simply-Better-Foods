using HarmonyLib;
using UnityEngine;

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
        public static void Food__ConsumedBy_Postfix(object[] __args, string ___Id)
        {
            Debug.LogError("Started Consume");
            for (int i = 0; i < FoodToEffects.Count; i++) {
                Debug.LogError("Started For loop");
                if (___Id == FoodToEffects[i].Id) {
                    Debug.LogError("Started If code");
                    switch (FoodToEffects[i].FoodEffects)
                    {
                        case FoodEffects.PermEffect:
                            break;
                        case FoodEffects.WellFed:
                            FoodEffect.WellFed((Combatable)__args[0]);
                            break;
                    }
                }
            }
        }

        private void Awake()
        {
            Harmony.PatchAll(typeof(SimplyBetterFoods));
            Harmony.PatchAll(typeof(FoodEffect));
        }
        public override void Ready()
        {
            AddFoodEffect(new FoodToEffect(FoodEffects.PermEffect, new FoodEffect(1), "apple"));
        }
    }
}