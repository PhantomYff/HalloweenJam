using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

using Random = UnityEngine.Random;

public static class WeightedRandom
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetRandomValue<T>(this IReadOnlyList<T> values)
        where T : IWeighted
    {
        return values[values.GetRandomIndex()];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetRandomValue<T, TArguments>(this IReadOnlyList<T> values, TArguments args)
        where T : IWeighted<TArguments>
    {
        return values[values.GetRandomIndex(args)];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRandomIndex<T>(this IReadOnlyList<T> values)
        where T : IWeighted
    {
        return GetRandomIndex_Internal(values, x => x.GetWeight());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRandomIndex<T, TArguments>(this IReadOnlyList<T> values, TArguments args)
        where T : IWeighted<TArguments>
    {
        return GetRandomIndex_Internal(values, x => x.GetWeight(args));
    }


    private static int GetRandomIndex_Internal<T>(IReadOnlyList<T> values, Func<T, float> weightSource)
    {
        var weights = CalculateWeights(values, weightSource);
        return FindRandomIndex(values, weights);

        static float[] CalculateWeights(IReadOnlyList<T> values, Func<T, float> weightSource)
        {
            var weights = new float[values.Count];

            for (int i = 0; i < values.Count; i++)
            {
                weights[i] = values[i] == null
                    ? 0f
                    : Mathf.Abs(weightSource.Invoke(values[i]));
            }

            return weights;
        }

        static int FindRandomIndex(IReadOnlyList<T> values, float[] weights)
        {
            float weightsSum = 0f;
            float randomWeight = Random.Range(0f, weights.Sum());

            for (var i = 0; i < values.Count; i++)
            {
                weightsSum += weights[i];

                if (randomWeight <= weightsSum)
                {
                    return i;
                }
            }
            throw new ArgumentException("Couldn't get random value.");
        }
    }
}
