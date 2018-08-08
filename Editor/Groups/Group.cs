using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace SKTools.ScriptingDefineManager.Groups
{
    [Serializable]
    public abstract class Group<T> : ScriptableObject, IGroup where T : ScriptableObject //
    {
        [FormerlySerializedAs("active")] [SerializeField]
        private bool _active;

        [FormerlySerializedAs("defines")] [SerializeField]
        private List<DefineSymbol> _defines;

        public abstract string Id { get; }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public IList Defines
        {
            get { return _defines; }
        }

        public virtual IGroup Clone()
        {
            var group = (IGroup) CreateInstance(_defines);
            group.Active = Active;
            return group;
        }

        protected static T CreateInstance(List<DefineSymbol> list)
        {
            var instance = (Group<T>) CreateInstance(typeof(T));
            instance._defines =
                list != null ? list.Select(i => new DefineSymbol(i)).ToList() : new List<DefineSymbol>();
            instance._active = false;
            return (T) (object) instance;
        }

        public override int GetHashCode()
        {
            var hash = Id.GetHashCode();
            _defines.ForEach(d => hash ^= d.GetHashCode());
            return hash;
        }

        public override bool Equals(object other)
        {
            return other != null && ((Group<T>) other).GetHashCode() == GetHashCode();
        }

        public override string ToString()
        {
            return string.Concat(GetType().Name, ": ", Id);
        }
    }
}