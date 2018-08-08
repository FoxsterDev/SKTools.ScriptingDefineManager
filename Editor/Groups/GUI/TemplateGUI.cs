using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SKTools.ScriptingDefineManager.Groups
{
    public class TemplateGUI : GroupGUI
    {
        private readonly List<DefineSymbol> _defines;
        private string _id;

        public TemplateGUI(List<DefineSymbol> defines) : base(defines)
        {
            _defines = defines;
        }

        protected override void DrawHeader(Rect rect)
        {
            var r = new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(r, "Create custom group: Input name:");
            r.x += r.width;
            _id = EditorGUI.TextField(r, _id);

            r.x += r.width;
            r.width = 100;
            if (GUI.Button(r, "Save")) // apply changes
                if (!string.IsNullOrEmpty(_id) && _defines.Count > 0)
                {
                    Debug.Log("create custom grop name");
                    var group = WithCustomName.Create(_id, _defines);
                    DefineSymbolManager.Apply(group);
                    DefineSymbolManager.Save(group);
                }

            r.x += r.width;
            if (GUI.Button(r, "Clear")) // apply changes
            {
                Debug.Log("clear");
                _defines.Clear();
            }
        }
    }
}