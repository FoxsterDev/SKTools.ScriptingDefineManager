using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace SKTools.ScriptingDefineManager.Groups
{
    public abstract class GroupGUI : IGroupGUI
    {
        protected ReorderableList _edited;
        protected IGroup _group;

        public GroupGUI(IGroup actual)
        {
            _group = actual;
            CreateEdited(actual.Defines);
        }

        public GroupGUI(List<DefineSymbol> list)
        {
            CreateEdited(list);
        }

        public virtual void Draw(ref float y, float width)
        {
            var height = _edited.GetHeight();
            _edited.DoList(new Rect(0, y, width, height));
            y += height;
        }

        private void CreateEdited(IList list)
        {
            _edited = new ReorderableList(list,
                typeof(DefineSymbol),
                true, true, true, true)
            {
                drawElementCallback = DrawElement,
                drawHeaderCallback = DrawHeader,
                onAddCallback = AddElement
            };
        }


        protected void AddElement(ReorderableList rlist)
        {
            if (rlist.list.Count < 1)
                rlist.list.Add(new DefineSymbol("Type your define symbol here!"));
            else
                rlist.list.Add(new DefineSymbol((DefineSymbol) rlist.list[rlist.list.Count - 1]));
        }

        protected virtual void DrawHeader(Rect rect)
        {
            var r = new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight);
            GUI.color = _group.Active ? Color.green : Color.yellow;
            if (GUI.Button(r, _group.Id)) // apply changes
                _group.Active = !_group.Active;
            r.x += r.width;
            GUI.color = Color.white;

            r.width = 100;
            if (GUI.Button(r, "Apply")) // apply changes
                if (!string.IsNullOrEmpty(_group.Id) && _edited.list.Count > 0) //???
                {
                    DefineSymbolManager.Apply(_group);
                    DefineSymbolManager.Save(_group);
                }

            r.x += r.width;
            if (GUI.Button(r, "Revert")) // apply changes
                Debug.Log("create custom grop name");
        }

        protected virtual void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = (DefineSymbol) _edited.list[index];
            var r = new Rect(rect.x, rect.y, 30, EditorGUIUtility.singleLineHeight);
            rect.y += 2;

            element.active = EditorGUI.Toggle(r, element.active);
            r.x += r.width;

            EditorGUI.BeginDisabledGroup(!element.active);

            r.width = 200;
            element.name = EditorGUI.TextField(r, GUIContent.none, element.name);

            EditorGUI.EndDisabledGroup();
        }
    }
}