using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Bootstraps
{
    public abstract class Bootstrap : MonoBehaviour
    {
        protected void Awake()
        {
            Run().RunAsync();
        }

        protected abstract Task Run();
    }
}
