using Juce.Core.Reflection.Utils;
using Juce.CoreUnity.SceneManagement.Group.CustomDrawers;
using Juce.CoreUnity.SceneManagement.Group.Data;
using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.SceneManagement.Group.Logic
{
    public static class GatherSceneEntryCustomDrawersLogic
    {
        public static void Execute(ToolData toolData)
        {
            toolData.SceneEntryCustomDrawers.Clear(); 

            List<Type> types = ReflectionUtils.GetInheritedTypes(typeof(ISceneEntryCustomDrawer));

            foreach(Type type in types)
            {
                bool hasDefaultConstructor = type.GetConstructor(Type.EmptyTypes) != null;

                if(!hasDefaultConstructor)
                {
                    continue;
                }

                ISceneEntryCustomDrawer customDrawer = Activator.CreateInstance(type) as ISceneEntryCustomDrawer;

                toolData.SceneEntryCustomDrawers.Add(customDrawer);
            }
        }
    }
}
