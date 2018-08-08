using System.Diagnostics;
using System.IO;
using EditorUtility.SKhaScriptingDefineManager;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SKTools.ScriptingDefineManager
{
    [InitializeOnLoad]
    public class DefineSymbolnitializer
    {
        static DefineSymbolnitializer()
        {
            CheckConstants();
            SyncWithPlayerSettings();
        }

        private static void CheckConstants()
        {
            if (string.IsNullOrEmpty(Constants.DirectoryPathInAssets) ||
                !Directory.Exists(Path.Combine(Application.dataPath,
                    Constants.DirectoryPathInAssets.Replace("Assets/", ""))))
            {
                Debug.Log("Update constants!");

                var stackTrace = new StackTrace(true);
                var directoryPath = stackTrace.GetFrames()[0].GetFileName()
                    .Replace(string.Format("{0}.cs", typeof(DefineSymbolnitializer).Name), "");
                var directoryPathInAssets =
                    directoryPath.Replace(Application.dataPath, "Assets").Replace(@"\", "/");
                var constants = File.ReadAllText(directoryPath + "ConstantsTemplate.template")
                    .Replace("{0}", directoryPathInAssets);

                File.WriteAllText(directoryPath + "Constants.cs", constants);
                Debug.Log("Constants was updated");
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
        }

        private static void SyncWithPlayerSettings()
        {
            Debug.Log("SyncWithPlayerSettings");

            var playerSettingsGroups = DefineSymbolManager.GetAllGroupsByBuildTargetGroupFromPlayerSettings();
            var savedGroups = DefineSymbolManager.GetAllGroupsByBuildTargetGroup(); //load all cashed presets
            //может нужен какой мерж групп..
            if (playerSettingsGroups.Count > 0) //????????
                foreach (var group in savedGroups)
                {
                    var index = playerSettingsGroups.FindIndex(g => g.TargetGroup == group.TargetGroup);
                    if (index > -1)
                    {
                        playerSettingsGroups.RemoveAt(index);
                        if (playerSettingsGroups.Count < 1) break;
                    }
                }

            //Debug.Log("except count : "+ groups.Count);   
            DefineSymbolManager.Apply(savedGroups); //apply presets
            playerSettingsGroups.ForEach(DefineSymbolManager.Save); //save some new build targets presets
        }
    }
}