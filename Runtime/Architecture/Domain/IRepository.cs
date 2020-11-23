namespace Juce.CoreUnity.Architecture
{
    public interface IRepository<T> where T : IEntity
    {
        T Get(int id);
    }
}