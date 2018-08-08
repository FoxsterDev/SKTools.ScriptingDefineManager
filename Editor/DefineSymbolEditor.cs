using System;
using System.Collections.Generic;
using SKTools.ScriptingDefineManager.Groups;
using UnityEditor;
using UnityEngine;

namespace SKTools.ScriptingDefineManager
{
    public class DefineSymbolEditor : EditorWindow
    {
        [Flags]
        public enum PlatformFilterType
        {
            // Decimal     // Binary
            None = 0, // 000000
            Obsolete = 1, // 000001
            Unsupported = 2 // 000010
        }

        private static readonly Dictionary<Type, Func<IGroup, IGroupGUI>> CreatorGroupGUI =
            new Dictionary<Type, Func<IGroup, IGroupGUI>>
            {
                {
                    typeof(WithBuildTargetGroupType),
                    group => new BuildTargetGroupTypeGUI((WithBuildTargetGroupType) group)
                },
                {
                    typeof(WithCustomName),
                    group => new CustomNameGUI((WithCustomName) group)
                }
            };

        private readonly List<IGroupGUI> _groupsGui = new List<IGroupGUI>();

        private string _groupBuildTargetFilter = "Categories";

        private PlatformFilterType _platformFilterType = PlatformFilterType.Obsolete | PlatformFilterType.Unsupported;

        [MenuItem("Window/DefineSymbolEditor Window")]
        private static void Init()
        {
            var window = (DefineSymbolEditor) GetWindow(typeof(DefineSymbolEditor));
            window.Show();
        }

        private void OnGUI()
        {
            var y = EditorGUIUtility.singleLineHeight;
            EditorGUILayout.BeginHorizontal();
            if (GUI.Button(new Rect(0, 0, 100, y), "Categories"))
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("Profiles"), _groupBuildTargetFilter == "Profiles",
                    obj => { _groupBuildTargetFilter = obj as string; },
                    "Profiles");
                menu.AddItem(new GUIContent("Platform/All"), _groupBuildTargetFilter == "Platforms_All",
                    obj => { _groupBuildTargetFilter = obj as string; },
                    "Platforms_All");

                menu.AddItem(new GUIContent("Platform/All/Filter/None"),
                    _platformFilterType == PlatformFilterType.None,
                    obj => { _platformFilterType = (PlatformFilterType) obj; },
                    PlatformFilterType.None);
                menu.AddItem(new GUIContent("Platform/All/Filter/Obsolete"),
                    (_platformFilterType & PlatformFilterType.Obsolete) != 0,
                    obj => { _platformFilterType |= (PlatformFilterType) obj; },
                    PlatformFilterType.Obsolete);
                menu.AddItem(new GUIContent("Platform/All/Filter/Unsupported"),
                    (_platformFilterType & PlatformFilterType.Unsupported) != 0,
                    obj => { _platformFilterType |= (PlatformFilterType) obj; },
                    PlatformFilterType.Unsupported);
                menu.ShowAsContext();
            }

            EditorGUILayout.EndHorizontal();

            foreach (var gui in _groupsGui) gui.Draw(ref y, position.width);
        }

        private void CreateAndAddDrawerToList(IGroup group)
        {
            if (CreatorGroupGUI.ContainsKey(group.GetType()))
            {
                var guiGroup = CreatorGroupGUI[group.GetType()].Invoke(group);
                _groupsGui.Add(guiGroup);
            }
            else
            {
                Debug.LogError("Cant find creator gui for type: " + group.GetType());
            }
        }

        private void OnEnable()
        {
            _groupsGui.Add(new TemplateGUI(new List<DefineSymbol>()));

            DefineSymbolManager.GetAllGroupsWithCustomName().ForEach(CreateAndAddDrawerToList);
            DefineSymbolManager.GetAllGroupsByBuildTargetGroup().ForEach(CreateAndAddDrawerToList);
        }
    }
}