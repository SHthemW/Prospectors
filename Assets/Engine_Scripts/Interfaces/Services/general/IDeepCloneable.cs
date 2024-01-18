namespace Game.Interfaces
{
    public interface IDeepCloneable<out T> where T : IDeepCloneable<T>
    {
        T DeepClone();
    }
}
