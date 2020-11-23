using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.UI
{
    public abstract class ViewModel : MonoBehaviour
    {
        public abstract Task Show();

        public abstract Task Hide();
    }
}