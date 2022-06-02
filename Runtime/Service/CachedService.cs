namespace Juce.CoreUnity.Service
{
    public struct CachedService<TService>
    {
        private bool isCached;
        private TService cachedService;

        public TService Value
        {
            get
            {
                if (isCached)
                {
                    return cachedService;
                }

                cachedService = ServiceLocator.Get<TService>();
                isCached = true;

                return cachedService;
            }
        }
    }
}