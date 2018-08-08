using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SKTools.ScriptingDefineManager
{
    public static class AssetUtil
    {
        private const char UnityAssetsDirectorySeparatorChar = '/';
        
      
        public static string GetAbsolutePath(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath) || !assetPath.Contains("Assets/"))
            {
                Debug.LogError("There are not in Asset folder: assetPath="+ assetPath);
                return "";
            }
           
            var path = Path.Combine(Application.dataPath, assetPath.Substring("Assets/".Length));

            if (Path.DirectorySeparatorChar != UnityAssetsDirectorySeparatorChar)
            {
                path = path.Replace(UnityAssetsDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return path;
        }

        public static string GetAssetPath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
            {
                Debug.LogError("This is not an absolute path: absolutePath="+ absolutePath);
                return "";
            }
            
            if (Path.DirectorySeparatorChar != UnityAssetsDirectorySeparatorChar)
            {
                absolutePath = absolutePath.Replace(Path.DirectorySeparatorChar , UnityAssetsDirectorySeparatorChar);
            }
            
            if (!absolutePath.Contains(Application.dataPath))
            {
                Debug.LogError("This is not an absolute path: absolutePath="+ absolutePath);
                return "";
            }
            
            var startIndex = Application.dataPath.Length - "Assets/".Length + 1;
            return absolutePath.Substring(startIndex);
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
            var files = Directory.GetFiles(Constants.AbsoluteDirectoryPathForPresets, searchPattern, SearchOption.AllDirectories);
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