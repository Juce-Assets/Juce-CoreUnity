using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.Core.UI
{
    public abstract class ViewModel : MonoBehaviour
    {
        private void Awake()
        {
            Bind();
        }

        protected virtual void Bind()
        {

        }

        public virtual Task Show()
        {
            return Task.CompletedTask;
        }

        public virtual Task Hide()
        {
            return Task.CompletedTask;
        }
    }
}
