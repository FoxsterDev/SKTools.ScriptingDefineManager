using System;
using UnityEditor;

namespace SKTools.ScriptingDefineManager
{
    public class FilterObsoletedBuildTargetGroup : IFilter
    {
        public bool Filter(object obj)
        {
            var field = typeof(BuildTargetGroup).GetField(obj.ToString());
            return field.IsDefined(typeof(ObsoleteAttribute), false);
        }
    }
}