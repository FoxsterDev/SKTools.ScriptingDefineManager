using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SKTools.ScriptingDefineManager
{
    public static partial class DefineSymbolManager
    {
        /// <summary>
        ///     <para>Set user-specified symbols for script compilation for the selected build target group.</para>
        /// </summary>
        /// <param name="defines">Array symbols for this group.</param>
        public static void Add(params string[] defines)
        {
            Add(EditorUserBuildSettings.selectedBuildTargetGroup, defines);
        }

        /// <summary>
        ///     <para>Set user-specified symbols for script compilation for the given build target group.</para>
        /// </summary>
        /// <param name="targetGroup">The name of the group of devices.</param>
        /// <param name="defines">Array symbols for this group.</param>
        public static void Add(BuildTargetGroup targetGroup, params string[] defines)
        {
            var definesCurrent = GetScriptingDefineSymbolsForGroup(targetGroup);
            var definesNew = defines.Except(definesCurrent).ToArray();

            if (definesNew.Length > 0)
            {
                definesCurrent = definesCurrent.Concat(definesNew).Where(s => !string.IsNullOrEmpty(s)).ToArray();
                SetScriptingDefineSymbolsForGroup(targetGroup, definesCurrent);
            }
        }

        /// <summary>
        ///     <para>Remove user-specified symbols for script compilation for the selected build target group.</para>
        /// </summary>
        /// <param name="defines">Array symbols for this group.</param>
        public static void Remove(params string[] defines)
        {
            Remove(EditorUserBuildSettings.selectedBuildTargetGroup, defines);
        }

        /// <summary>
        ///     <para>Remove user-specified symbols for script compilation for the given build target group.</para>
        /// </summary>
        /// <param name="targetGroup">The name of the group of devices.</param>
        /// <param name="defines">Array symbols for this group.</param>
        public static void Remove(BuildTargetGroup targetGroup, params string[] defines)
        {
            var definesCurrent = GetScriptingDefineSymbolsForGroup(targetGroup);

            var definesNew = definesCurrent.Except(defines).ToArray();
            if (definesNew.Length < definesCurrent.Length)
            {
                SetScriptingDefineSymbolsForGroup(targetGroup, definesNew);
            }
        }

        public static IGroup GetGroup(string yourGroupName)
        {
            return default(IGroup);
        }

        /// <summary>
        ///     <para>Set user-specified symbols for script compilation for the given build target group.</para>
        /// </summary>
        /// <param name="targetGroup">The name of the group of devices.</param>
        /// <param name="defines">Array symbols for this group.</param>
        private static void SetScriptingDefineSymbolsForGroup(BuildTargetGroup targetGroup, string[] defines)
        {
            var definesStr = string.Join(";", defines);
            Debug.Log(definesStr);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, definesStr);
        }

        /// <summary>
        ///     <para>Get user-specified symbols for script compilation for the given build target group.</para>
        /// </summary>
        /// <param name="targetGroup">The name of the group of devices.</param>
        private static string[] GetScriptingDefineSymbolsForGroup(BuildTargetGroup targetGroup)
        {
            var definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            if (string.IsNullOrEmpty(definesString))
            {
                return new string[0];
            }

            return definesString.Split(';').ToArray();
        }
    }
}
