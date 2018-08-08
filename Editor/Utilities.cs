using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SKTools.ScriptingDefineManager
{
    public static class Utilities
    {
        private static string GetAbsolutePath(string assetPath)
        {
            return Path.Combine(Application.dataPath, assetPath.Replace("Assets/", ""));
        }

        public static string GetAbsoluteDirectoryPathForPresets()
        {
            return GetAbsolutePath(GetDirectoryPathForPresets());
        }

        public static string GetDirectoryPathForPresets()
        {
            var directoryPath = string.Concat(Constants.DirectoryPathInAssets, "Presets/");
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            return directoryPath;
        }

        public static void RefreshAssetAtPath<T>(T assetNew, string assetPath) where T : Object
        {
            var assetExisting = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            if (assetExisting != null) AssetDatabase.DeleteAsset(assetPath);

            AssetDatabase.CreateAsset(assetNew, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static List<T> LoadAllAssetsAtPresetsFolder<T>(string searchPattern = "") where T : Object
        {
            var absolutePathFolder = GetAbsoluteDirectoryPathForPresets();

            var files = Directory.GetFiles(absolutePathFolder, searchPattern, SearchOption.AllDirectories);
            var list = new List<T>();
            var startIndex = Application.dataPath.Length - "Assets/".Length + 1;

            foreach (var file in files)
            {
                if (file.EndsWith(".meta")) continue;

                var assetPath = file.Substring(startIndex);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset == null)
                    continue;
                //Debug.Log(asset.ToString() + ":: " + asset.GetHashCode());
                list.Add(asset);
            }

            return list;
        }
    }
}