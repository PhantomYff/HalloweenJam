public interface IWeighted
{
    public float GetWeight();
}

public interface IWeighted<TArguments>
{
    public float GetWeight(TArguments args);
}
