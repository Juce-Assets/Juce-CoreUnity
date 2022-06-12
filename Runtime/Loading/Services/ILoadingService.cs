namespace Juce.CoreUnity.Loading.Services
{
    public interface ILoadingService
    {
        bool IsLoading { get; }

        void StartsLoading();
        void StopsLoading();
    }
}
