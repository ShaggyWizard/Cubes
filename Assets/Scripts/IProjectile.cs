public interface IProjectile
{
    public float Speed { get; }
    public float MaxDistance { get; }


    public void SetSpeed(float newSpeed);
    public void SetDistance(float newDistance);
}