using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SKTools.ScriptingDefineManager.Groups
{
    [Serializable]
    public class WithCustomName : Group<WithCustomName>
    {
        [FormerlySerializedAs("id")] [SerializeField]
        private string _id;

        public override string Id
        {
            get { return _id; }
        }

        public static WithCustomName Create(string id, List<DefineSymbol> list)
        {
            var instance = CreateInstance(list);
            instance._id = id;
            return instance;
        }

        public override IGroup Clone()
        {
            var clone = (WithCustomName) base.Clone();
            clone._id = _id;
            return clone;
        }
    }
}