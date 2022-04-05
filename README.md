# Juce-CoreUnity

## Installing

Unity Package Manager dependences:
- TextMeshPro

### - Via UPM
Unity does not support resolving dependences from a git url. Because of that, you will need to add the following lines to your [manifest.json](https://docs.unity3d.com/Manual/upm-manifestPrj.html).
```
"dependencies": {
   "com.juce.utils": "git+https://github.com/Juce-Assets/Juce-Utils",
   "com.juce.core": "git+https://github.com/Juce-Assets/Juce-Core",
   "com.juce.tween": "git+https://github.com/Juce-Assets/Juce-Core",
   "com.juce.scenemanagement": "git+https://github.com/Juce-Assets/Juce-SceneManagement",
   "com.juce.tweencomponent": "git+https://github.com/Juce-Assets/Juce-TweenPlayer",
   "com.juce.input": "git+https://github.com/Juce-Assets/Juce-Input"
},
```
