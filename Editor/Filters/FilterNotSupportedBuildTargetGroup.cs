using System;
using System.Reflection;
using UnityEditor;

namespace SKTools.ScriptingDefineManager
{
    public class FilterNotSupportedBuildTargetGroup : IFilter
    {
        public bool Filter(object obj)
        {
            var group = (BuildTargetGroup) obj;
            if (group == BuildTargetGroup.Standalone)
            {
                return true;
            }

            var moduleManager = Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");
            var isPlatformSupportLoaded = moduleManager.GetMethod(
                "IsPlatformSupportLoaded",
                BindingFlags.Static | BindingFlags.NonPublic);

            var getTargetStringFromBuildTargetGroup = moduleManager.GetMethod(
                "GetTargetStringFromBuildTargetGroup",
                BindingFlags.Static | BindingFlags.NonPublic);

            return (bool) isPlatformSupportLoaded.Invoke(
                null,
                new object[] { (string) getTargetStringFromBuildTargetGroup.Invoke(null, new object[] { group }) });
        }
    }
}
