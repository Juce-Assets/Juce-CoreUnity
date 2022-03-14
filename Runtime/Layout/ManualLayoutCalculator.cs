namespace Juce.CoreUnity.Layout
{
    public static class ManualLayoutCalculator
    {
        public static float Calculate(
            float startingPosition,
            int elementsCount,
            int elementIndex, 
            float distanceBetweenElements,
            ManualLayoutAlignment alignment
            )
        {
            if(elementsCount == 0)
            {
                return startingPosition;
            }

            switch(alignment)
            {
                default:
                case ManualLayoutAlignment.LeftOrDown:
                    {
                        return startingPosition + (elementIndex * distanceBetweenElements);
                    }

                case ManualLayoutAlignment.RightOrUp:
                    {
                        return startingPosition - (elementIndex * distanceBetweenElements);
                    }

                case ManualLayoutAlignment.Center:
                    {
                        float actualStartingPosition = startingPosition - (((elementsCount - 1) * distanceBetweenElements) * 0.5f);

                        return actualStartingPosition + (elementIndex * distanceBetweenElements);
                    }
            }
        }

        public static float Calculate(
            float startingPosition,
            int elementsCount,
            int elementIndex,
            float distanceBetweenElements,
            ManualLayoutHorizontalAlignment alignment
            )
        {
            ManualLayoutAlignment actualAlignment = ManualLayoutAlignment.LeftOrDown;

            switch (alignment)
            {
                default:
                case ManualLayoutHorizontalAlignment.Left:
                    {
                        actualAlignment = ManualLayoutAlignment.LeftOrDown;
                        break;
                    }

                case ManualLayoutHorizontalAlignment.Right:
                    {
                        actualAlignment = ManualLayoutAlignment.RightOrUp;
                        break;
                    }

                case ManualLayoutHorizontalAlignment.Center:
                    {
                        actualAlignment = ManualLayoutAlignment.Center;
                        break;
                    }
            }

            return Calculate(
                startingPosition,
                elementsCount,
                elementIndex,
                distanceBetweenElements,
                actualAlignment
                );
        }

        public static float Calculate(
            float startingPosition,
            int elementsCount,
            int elementIndex,
            float distanceBetweenElements,
            ManualLayoutVerticalAlignment alignment
            )
        {
            ManualLayoutAlignment actualAlignment = ManualLayoutAlignment.LeftOrDown;

            switch (alignment)
            {
                default:
                case ManualLayoutVerticalAlignment.Down:
                    {
                        actualAlignment = ManualLayoutAlignment.LeftOrDown;
                        break;
                    }

                case ManualLayoutVerticalAlignment.Up:
                    {
                        actualAlignment = ManualLayoutAlignment.RightOrUp;
                        break;
                    }

                case ManualLayoutVerticalAlignment.Center:
                    {
                        actualAlignment = ManualLayoutAlignment.Center;
                        break;
                    }
            }

            return Calculate(
                startingPosition,
                elementsCount,
                elementIndex,
                distanceBetweenElements,
                actualAlignment
                );
        }
    }
}

