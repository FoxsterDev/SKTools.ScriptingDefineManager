using System.Collections.Generic;

namespace SKTools.ScriptingDefineManager
{
    public class Filter
    {
        public static readonly IFilter NotSupportedBuildTargetGroup = new FilterNotSupportedBuildTargetGroup();
        public static readonly IFilter ObsoletedBuildTargetGroup = new FilterObsoletedBuildTargetGroup();

        public static List<T> Apply<T>(List<T> source, params IFilter[] filters)
        {
            return null;
        }
    }
}