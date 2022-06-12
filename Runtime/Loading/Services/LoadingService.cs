namespace Juce.CoreUnity.Loading.Services
{
    public class LoadingService : ILoadingService
    {
        public bool IsLoading { get; private set; }

        public void StartsLoading()
        {
            IsLoading = true;
        }

        public void StopsLoading()
        {
            IsLoading = false;
        }
    }
}
