using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class NegentropyArray<T>
{
    private readonly Item[] _values;
    private readonly float _weightDecrement;
    private readonly float _weightRecovery;

    private const float DEFAULT_WEIGHT_DECREMENT = 0.5f;
    private const float DEFAULT_WEIGHT_RECOVERY = 0.2f;
    private const float DEFAULT_WEIGHT = 1f;

    private NegentropyArray(IEnumerable<T> values, float weightDecrement, float weightRecovery)
    {
        _values = values
            .Select(x => new Item(x))
            .ToArray();

        _weightDecrement = weightDecrement;
        _weightRecovery = weightRecovery;
    }

    public T GetRandomValue()
    {
        int choosenIndex = _values.GetRandomIndex();

        for (int i = 0; i < _values.Length; i++)
        {
            if (_values[i].weight != DEFAULT_WEIGHT)
            {
                _values[i].weight = Mathf.Max(
                    _values[i].weight + _weightRecovery,
                    DEFAULT_WEIGHT);
            }

            if (i == choosenIndex)
            {
                _values[i].weight -= _weightDecrement;
            }
        }

        return _values[choosenIndex].value;
    }

    public static NegentropyArray<T> New(IEnumerable<T> values)
    {
        return Builder.New(values).Build();
    }

    public sealed class Builder
    {
        private Builder(IEnumerable<T> values)
        {
            _values = values.ToList();
        }

        private readonly List<T> _values;

        private float _weightDecrement = DEFAULT_WEIGHT_DECREMENT;
        private float _weightRecovery = DEFAULT_WEIGHT_RECOVERY;

        public Builder WeightDecrement(float weightDecrement)
        {
            AssertMin(weightDecrement, nameof(weightDecrement), 0f);
            AssertMax(weightDecrement, nameof(weightDecrement), 1f);

            _weightDecrement = weightDecrement;

            return this;
        }

        public Builder WeightRecovery(float weightRecovery)
        {
            AssertMin(weightRecovery, nameof(weightRecovery), 0f);

            _weightRecovery = weightRecovery;

            return this;
        }

        public Builder AddValue(T value)
        {
            _values.Add(value);

            return this;
        }

        public Builder AddRange(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            _values.AddRange(values);

            return this;
        }

        public NegentropyArray<T> Build()
        {
            return new NegentropyArray<T>(_values, _weightDecrement, _weightRecovery);
        }

        public static Builder New(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            if (values.Any() == false)
            {
                throw new ArgumentException($"\"{nameof(values)}\" should contain at least 1 item.");
            }

            return new Builder(values);
        }

        private static void AssertMin(float value, string parameterName, float min)
        {
            if (value < min)
            {
                throw new ArgumentException($"\"{parameterName}\" has to be equal to or greater than {min}.");
            }
        }

        private static void AssertMax(float value, string parameterName, float max)
        {
            if (value > max)
            {
                throw new ArgumentException($"\"{parameterName}\" has to be equal to or less than {max}.");
            }
        }
    }

    private struct Item : IWeighted
    {
        public Item(T value)
        {
            this.value = value;

            weight = DEFAULT_WEIGHT;
        }

        public float weight;

        public readonly T value;

        public float GetWeight() => weight;
    }
}
