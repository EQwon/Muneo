using System.Collections.Generic;

namespace MuneoCrepe
{
    public static class ConfigGame
    {
        public const int MaximumLife = 5;
        public const int MaximumPassedCount = 2;
        public const float BeltCycleDuration = 0.6f;
        public const float InputDuration = 1.4f;

        public static readonly List<int> TimeLimitList = new List<int> {30, 50, 80, 120};
        public static readonly List<int> TargetAmountList = new List<int> {6, 8, 10, 10};
    }
}