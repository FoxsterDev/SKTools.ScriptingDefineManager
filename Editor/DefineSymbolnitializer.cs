using System.Diagnostics;
using System.IO;
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
            UpdateConstants();
            SyncWithPlayerSettings();
        }

        private static void UpdateConstants()
        {
            if (string.IsNullOrEmpty(Constants.DirectoryPathInAssets) ||
                !Directory.Exists(Constants.DirectoryPathInAssets))
            {
                Debug.Log("Update constants!");

                var stackTrace = new StackTrace(true);
                var directoryPath = stackTrace.GetFrames()[0].GetFileName()
                    .Replace(string.Format("{0}.cs", typeof(DefineSymbolnitializer).Name), "");

                Debug.Log(directoryPath);
                Constants.DirectoryPathInAssets = AssetUtil.GetAssetPath(directoryPath);
                Constants.AssetDirectoryPathForPresets = string.Concat(Constants.DirectoryPathInAssets, "Presets/");
                Constants.AbsoluteDirectoryPathForPresets = AssetUtil.GetAbsolutePath(Constants.AssetDirectoryPathForPresets);

                if (!Directory.Exists(Constants.AbsoluteDirectoryPathForPresets))
                {
                    Directory.CreateDirectory(Constants.AbsoluteDirectoryPathForPresets);
                }
                
                var template = File.ReadAllText(directoryPath + "ConstantsTemplate.template");
                var constants = template
                    .Replace("{0}", Constants.DirectoryPathInAssets);
                constants = constants
                    .Replace("{1}", Constants.AssetDirectoryPathForPresets);
                constants = constants
                    .Replace("{2}", Constants.AbsoluteDirectoryPathForPresets);

                Debug.Log("Constants was updated");

                File.WriteAllText(directoryPath + "Constants.cs", constants);
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