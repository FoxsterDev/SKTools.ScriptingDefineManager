using System;
using UnityEngine.Serialization;

namespace SKTools.ScriptingDefineManager
{
    [Serializable]
    public class DefineSymbol
    {
        [FormerlySerializedAs("active")]
        public bool active = true;

        [FormerlySerializedAs("buildtargetmask") /*, HideInInspector*/]
        public CustomBuildTarget BuildTargetMask = CustomBuildTarget.Unknown;

        [FormerlySerializedAs("name")]
        public string name;

        public DefineSymbol(DefineSymbol s)
        {
            name = s.name;
            active = s.active;
            BuildTargetMask = s.BuildTargetMask;
        }

        public DefineSymbol(string s)
        {
            name = s.ToUpper();
        }

        public DefineSymbol()
        {
        }

        public override int GetHashCode()
        {
            return !string.IsNullOrEmpty(name)
                       ? name.GetHashCode()
                       : 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (!(obj is DefineSymbol))
            {
                return false;
            }

            return obj != null && name.Equals(((DefineSymbol) obj).name);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
