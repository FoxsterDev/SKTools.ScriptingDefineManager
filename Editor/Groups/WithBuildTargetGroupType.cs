using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace SKTools.ScriptingDefineManager.Groups
{
    [Serializable]
    public class WithBuildTargetGroupType : Group<WithBuildTargetGroupType>
    {
        [FormerlySerializedAs("targetgroup")] [SerializeField]
        private BuildTargetGroup _targetGroup;

        public BuildTargetGroup TargetGroup
        {
            get { return _targetGroup; }
        }

        public override string Id
        {
            get { return _targetGroup.ToString(); }
        }

        public static WithBuildTargetGroupType Create(BuildTargetGroup targetGroup, List<DefineSymbol> list)
        {
            var instance = CreateInstance(list);
            instance._targetGroup = targetGroup;
            return instance;
        }

        public override IGroup Clone()
        {
            var clone = (WithBuildTargetGroupType) base.Clone();
            clone._targetGroup = _targetGroup;
            return clone;
        }
    }
}