using System;
using System.Collections.Generic;
using System.Reflection;

namespace Assets.Juce_SceneManagement.Editor.Group.Utils
{
    public static class ReflectionUtils
    {
        public static List<Type> GetInheritedTypes(Type baseType, bool includeAbstractsAndInterfaces = false)
        {
            List<Type> ret = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if(type.IsAbstract || type.IsInterface)
                    {
                        continue;
                    }

                    if (!baseType.IsAssignableFrom(type))
                    {
                        continue;
                    }

                    if (baseType == type)
                    {
                        continue;
                    }

                    if (!includeAbstractsAndInterfaces && (type.IsAbstract || type.IsInterface))
                    {
                        continue;
                    }

                    ret.Add(type);
                }
            }

            return ret;
        }
    }
}
