using UnityEditor;
using UnityEngine;

namespace SKTools.ScriptingDefineManager.Groups
{
    public class CustomNameGUI : GroupGUI
    {
        public CustomNameGUI(WithCustomName actual)
            : base(actual)
        {
        }

        protected override void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            base.DrawElement(rect, index, isActive, isFocused);

            rect.x += 300;

            var element = (DefineSymbol) _edited.list[index];

            element.BuildTargetMask =
                (CustomBuildTarget) EditorGUI.EnumMaskField(
                    new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),
                    element.BuildTargetMask);
        }
    }
}
