using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Utilities
{
    public static class FixedProbabilisticTriggering
    {
        public static void PerformOnAverageOnceEvery(float howManySeconds, Action actionToPerform)
        {
            var fixedUpdatesPerDeclaredTime = howManySeconds/Time.fixedDeltaTime;
            var randomizedValue = Random.value*fixedUpdatesPerDeclaredTime;
            if (TestOnAverageOnceEvery(howManySeconds))
            {
                actionToPerform();
            }
        }

        public static bool TestOnAverageOnceEvery(float howManySeconds)
        {
            var fixedUpdatesPerDeclaredTime = howManySeconds/Time.fixedDeltaTime;
            var randomizedValue = Random.value*fixedUpdatesPerDeclaredTime;
            return randomizedValue < 1;
        }
    }

    public static class ProbabilisticTriggering
    {
        public static void PerformOnAverageOnceEvery(float howManySeconds, Action actionToPerform)
        {
            var updatesPerDeclaredTime = howManySeconds/Time.deltaTime;
            var randomizedValue = Random.value*updatesPerDeclaredTime;
            if (TestOnAverageOnceEvery(howManySeconds))
            {
                actionToPerform();
            }
        }

        public static bool TestOnAverageOnceEvery(float howManySeconds)
        {
            var updatesPerDeclaredTime = howManySeconds/Time.deltaTime;
            var randomizedValue = Random.value*updatesPerDeclaredTime;
            return randomizedValue < 1;
        }

        public static bool TestProbabilisty(float probabilty)
        {
            var randomizedValue = Random.value;
            return randomizedValue < probabilty;
        }
    }
}