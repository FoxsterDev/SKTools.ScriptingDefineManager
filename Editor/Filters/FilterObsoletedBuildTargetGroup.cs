using System;
using NUnit.Framework;
using UnityEditor;

namespace SKTools.ScriptingDefineManager
{
    public class FilterObsoletedBuildTargetGroup : IFilter
    {
        public bool Filter(object obj)
        {
            Assert.IsAssignableFrom<BuildTargetGroup>(obj);

            var field = typeof(BuildTargetGroup).GetField(obj.ToString());
            return field.IsDefined(typeof(ObsoleteAttribute), false);
        }
    }
}