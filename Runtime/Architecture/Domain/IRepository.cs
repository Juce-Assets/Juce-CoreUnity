namespace Juce.Core.Architecture
{
    public interface IRepository<T> where T : IEntity
    {
        T Get(int id);
    }
}