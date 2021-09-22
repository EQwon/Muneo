using System.Collections.Generic;

namespace MuneoCrepe.Extensions
{
    public static class MuneoUtil
    {
        public static (int t1, int t2, int t3, int t4) ConvertToInts(this Dictionary<MuneoType, int> dict)
        {
            return (dict[MuneoType.Color], dict[MuneoType.Hat], dict[MuneoType.Dyeing], dict[MuneoType.Eye]);
        }
        
        public static (int t1, int t2, int t3, int t4) ConvertToInts(this Dictionary<IngredientType, int> dict)
        {
            return (dict[IngredientType.Cone], dict[IngredientType.Fruit], dict[IngredientType.Syrup], dict[IngredientType.Topping]);
        }
    }
}
