using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public struct Interval : IEquatable<Interval>
{
    public Interval(float minimum, float maximum)
    {
        _min = minimum;
        _max = maximum;

#if DEBUG && UNITY_EDITOR
        AssertMinLessThanMax();
#endif
    }

    public Interval((float, float) bounds)
    {
        _min = bounds.Item1;
        _max = bounds.Item2;

#if DEBUG && UNITY_EDITOR
        AssertMinLessThanMax();
#endif
    }

    public Interval(Tuple<float, float> bounds)
    {
        _min = bounds.Item1;
        _max = bounds.Item2;

#if DEBUG && UNITY_EDITOR
        AssertMinLessThanMax();
#endif
    }

    [SerializeField] private float _min;
    [SerializeField] private float _max;

    private readonly static Func<float, float> s_floatSorting = x => x;

    public float Min
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _min;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _min = value;

#if DEBUG && UNITY_EDITOR
            AssertMinLessThanMax();
#endif
        }
    }

    public float Max
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _max;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _max = value;

#if DEBUG && UNITY_EDITOR
            AssertMinLessThanMax();
#endif
        }
    }

    public float Random
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => UnityEngine.Random.Range(_min, _max);
    }

    public float Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _max - _min;
    }

    public float Middle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _min + Length / 2;
    }

    public bool IsInteger
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _min == (int)_min && _max == (int)_max;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Includes(float value)
    {
        return _min <= value && value <= _max;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Includes(Interval other)
    {
        return this._min <= other._min && other._max <= this._max;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersects(Interval other)
    {
        return this._min <= other._max || this._max >= other._min;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Interval Clamped(float min, float max)
    {
        min = Mathf.Max(min, _min);
        max = Mathf.Min(max, _max);

        return new Interval(min, max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clamp(float min, float max)
    {
        _min = Mathf.Max(min, _min);
        _max = Mathf.Min(max, _max);

#if DEBUG && UNITY_EDITOR
        AssertMinLessThanMax();
#endif
    }

    public Interval[] Split(params float[] separators)
    {
        if (separators.Length == 0)
            return new Interval[] { this };

        LinkedList<Interval> result = new();
        foreach (float point in separators.OrderBy(s_floatSorting))
        {
            if (point > _min)
            {
                result.AddLast(result.Count == 0
                    ? new Interval(_min, point)
                    : new Interval(result.Last.Value._max, point));

                if (point >= _max)
                {
                    result.AddLast(new Interval(result.Last.Value._max, _max));
                    break;
                }
            }
        }

        return result.Count == 0
            ? new Interval[] { this }
            : result.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Interval other)
    {
        return this._min == other._min && other._max == this._max;
    }

    public override bool Equals(object obj)
    {
        return obj is Interval interval && this.Equals(interval);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_min, _max);
    }

    public override string ToString()
    {
        return $"({_min}; {_max})";
    }

#if DEBUG && UNITY_EDITOR
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AssertMinLessThanMax()
    {
        if (_max <= _min)
        {
            throw new ArgumentOutOfRangeException($"\"{nameof(Max)}\" cannot be less than or equal to \"{nameof(Min)}\".");
        }
    }
#endif

    public static bool operator ==(Interval obj, Interval other) => obj.Equals(other);

    public static bool operator !=(Interval obj, Interval other) => (obj == other) == false;
}
