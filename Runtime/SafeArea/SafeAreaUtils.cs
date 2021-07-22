using UnityEngine;

namespace Juce.CoreUnity.SafeArea
{
    public static class SafeAreaUtils
    {
        /// <summary>
        /// We calculate the number of 'rotations' that we have to do to for the reference orientation
        /// to match the current orientation
        /// A 'rotation' means rotating the phone 90º
        /// We rotate clockwise until we find the desired orientation. The ammount of rotations are the value returned
        /// </summary>
        public static int GetReferenceOrientationDiff(ScreenSafeAreaConfiguration settings, ScreenOrientation orientation)
        {
            switch (settings.ReferenceOrientation)
            {
                case ScreenOrientation.Portrait:
                    {
                        switch (orientation)
                        {
                            case ScreenOrientation.Portrait:
                                {
                                    return 0;
                                }

                            case ScreenOrientation.LandscapeRight:
                                {
                                    return 1;
                                }

                            case ScreenOrientation.PortraitUpsideDown:
                                {
                                    return 2;
                                }

                            case ScreenOrientation.LandscapeLeft:
                                {
                                    return 3;
                                }
                        }

                        break;
                    }

                case ScreenOrientation.LandscapeRight:
                    {
                        switch (orientation)
                        {
                            case ScreenOrientation.Portrait:
                                {
                                    return 3;
                                }

                            case ScreenOrientation.LandscapeRight:
                                {
                                    return 0;
                                }

                            case ScreenOrientation.PortraitUpsideDown:
                                {
                                    return 1;
                                }

                            case ScreenOrientation.LandscapeLeft:
                                {
                                    return 2;
                                }
                        }

                        break;
                    }

                case ScreenOrientation.PortraitUpsideDown:
                    {
                        switch (orientation)
                        {
                            case ScreenOrientation.Portrait:
                                {
                                    return 2;
                                }

                            case ScreenOrientation.LandscapeRight:
                                {
                                    return 3;
                                }

                            case ScreenOrientation.PortraitUpsideDown:
                                {
                                    return 0;
                                }

                            case ScreenOrientation.LandscapeLeft:
                                {
                                    return 1;
                                }
                        }

                        break;
                    }

                case ScreenOrientation.LandscapeLeft:
                    {
                        switch (orientation)
                        {
                            case ScreenOrientation.Portrait:
                                {
                                    return 1;
                                }

                            case ScreenOrientation.LandscapeRight:
                                {
                                    return 2;
                                }

                            case ScreenOrientation.PortraitUpsideDown:
                                {
                                    return 3;
                                }

                            case ScreenOrientation.LandscapeLeft:
                                {
                                    return 0;
                                }
                        }

                        break;
                    }
            }

            return 0;
        }
    }
}