using System;

namespace Juce.Core.UI
{
    public class Binding
    {
        private bool binded;

        public void Bind()
        {
            if(!binded)
            {
                binded = true;

                OnBind();
            }
        }

        public void Unbind()
        {
            if(binded)
            {
                binded = false;

                OnUnbind();
            }
        }

        protected virtual void OnBind()
        {

        }

        protected virtual void OnUnbind()
        {

        }
    }
}
