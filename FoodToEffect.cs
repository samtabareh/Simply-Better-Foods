using static SimplyBetterFoodsNS.FoodEffect;

namespace SimplyBetterFoodsNS
{
    public struct FoodToEffect
    {
        public FoodEffects FoodEffects;
        public FoodEffect FoodEffect = new FoodEffect();
        public string Id;

        public FoodToEffect(FoodEffects FoodEffects, FoodEffect FoodEffect, string Id) 
        {
            this.FoodEffects = FoodEffects;
            this.FoodEffect = FoodEffect;
            this.Id = Id;
        }
    }
}
