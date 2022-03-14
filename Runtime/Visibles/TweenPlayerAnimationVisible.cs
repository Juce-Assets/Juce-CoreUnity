using Juce.Core.Visibility;
using Juce.CoreUnity.TweenComponent;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Visibles
{
    public class TweenPlayerAnimationVisible : IVisible
    {
        private readonly TweenPlayerAnimation showAnimation;
        private readonly TweenPlayerAnimation hideAnimation;

        public TweenPlayerAnimationVisible(
            TweenPlayerAnimation showAnimation,
            TweenPlayerAnimation hideAnimation
            )
        {
            this.showAnimation = showAnimation;
            this.hideAnimation = hideAnimation;
        }

        public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        {
            if(visible)
            {
                return showAnimation.Execute(instantly, cancellationToken);
            }

            return hideAnimation.Execute(instantly, cancellationToken);
        }
    }
}
