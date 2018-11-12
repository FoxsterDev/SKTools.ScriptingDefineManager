using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SKTools.ScriptingDefineManager.Groups;
using UnityEditor;
using UnityEngine;

namespace SKTools.ScriptingDefineManager
{
    public static partial class DefineSymbolManager
    {
        public static bool HasBuildTarget(CustomBuildTarget mask, CustomBuildTarget target)
        {
            return (mask & target) == target;
        }
        //*************************************

        internal static void Apply(IGroup group)
        {
            var defines = GetDefineAggregatingString(group.Defines);

            if (group is WithBuildTargetGroupType)
            {
                SetScriptingDefineSymbolsForGroup((WithBuildTargetGroupType) group, defines);
            }
            else if (group is WithCustomName)
            {
                /*
                 *
                 *  
                 */
            }
        }

        private static void SetScriptingDefineSymbolsForGroup(WithBuildTargetGroupType group, string[] defines)
        {
            SetScriptingDefineSymbolsForGroup(group.TargetGroup, defines);
        }

        private static void SetScriptingDefineSymbolsForGroup(WithCustomName group, string[] defines)
        {
            //group.
            //SetScriptingDefineSymbolsForGroup(group.TargetGroup, defines);
        }

        private static string[] GetDefineAggregatingString(IList defines)
        {
            return defines.Cast<DefineSymbol>().Where(d => d.active).Select(d => d.ToString()).ToArray();
        }

        //*****************************************************

        internal static string GetAssetPath(string assetId)
        {
            return string.Concat(Constants.AssetDirectoryPathForPresets, assetId, ".asset");
        }

        internal static IGroup GetAsset(string id)
        {
            var assetPath = GetAssetPath(id);
            return (IGroup) AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
        }

        internal static void Save(IGroup group)
        {
            var assetPath = GetAssetPath(group.Id);
            var assetNew = (ScriptableObject) group.Clone();

            AssetUtil.RefreshAssetAtPath(assetNew, assetPath);
        }

        internal static void Apply(List<WithBuildTargetGroupType> list)
        {
            list.ForEach(Apply);
        }

        internal static List<WithCustomName> GetAllGroupsWithCustomName()
        {
            return LoadAllPresetsAtFolder<WithCustomName>();
        }

        internal static List<WithBuildTargetGroupType> GetAllGroupsByBuildTargetGroup()
        {
            return LoadAllPresetsAtFolder<WithBuildTargetGroupType>();
        }

        internal static List<WithBuildTargetGroupType> GetAllGroupsByBuildTargetGroupFromPlayerSettings()
        {
            var list = new List<WithBuildTargetGroupType>();
            var allBuildTargetGroups = Enum.GetValues(typeof(BuildTargetGroup));

            foreach (BuildTargetGroup targetGroup in allBuildTargetGroups)
            {
                var definesForGroup = GetScriptingDefineSymbolsForGroup(targetGroup);
                var defines = definesForGroup.Select(s => new DefineSymbol(s)).ToList();
                if (defines.Count < 1)
                {
                    continue;
                }

                var group = WithBuildTargetGroupType.Create(targetGroup, defines);
                list.Add(group);
            }

            return list;
        }

        private static List<T> LoadAllPresetsAtFolder<T>() where T : ScriptableObject, IGroup
        {
            var assets = AssetUtil.LoadAllAssetsAtPresetsFolder<T>("*.asset");

            var list = new List<T>();
            foreach (var asset in assets)
            {
                list.Add((T) asset.Clone());
            }

            return list;
        }
    }
}
