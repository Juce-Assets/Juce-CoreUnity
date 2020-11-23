<<<<<<< HEAD
﻿namespace Juce.Core.Architecture
=======
﻿namespace Juce.CoreUnity.Architecture
>>>>>>> develop
{
    public interface IRepository<T> where T : IEntity
    {
        T Get(int id);
    }
}