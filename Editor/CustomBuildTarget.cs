using System;

namespace SKTools.ScriptingDefineManager
{
    /// <summary>
    ///     <para>Custom build target group.</para>
    /// </summary>
    [Flags]
    public enum CustomBuildTarget
    {
        Unknown = 0,
        All = ~0,

        Standalone = 1 << 0,

        iOS = 1 << 2,
        iPhone = 1 << 3,
        tvOS = 1 << 4,

        Android = 1 << 6,
        Tizen = 1 << 7,
        SamsungTV = 1 << 8,

        Metro = 1 << 10,
        WSA = 1 << 11,
        WP8 = 1 << 12,

        WebPlayer = 1 << 13, //removed in 5.4
        WebGL = 1 << 14,

        Facebook = 1 << 20,
        WiiU = 1 << 21,
        Switch = 1 << 22,

        PSP2 = 1 << 23,
        PS3 = 1 << 24,
        PS4 = 1 << 25,
        PSM = 1 << 26,

        BlackBerry = 1 << 27,

        XBOX360 = 1 << 28,
        XboxOne = 1 << 29,

        N3DS = 1 << 30
    }
}

/*
 *
 *  public enum BuildTargetGroup
  {
    Unknown = 0,
    Standalone = 1,
    [Obsolete("WebPlayer was removed in 5.4, consider using WebGL", true)] WebPlayer = 2,
    iOS = 4,
    [Obsolete("Use iOS instead (UnityUpgradable) -> iOS", true)] iPhone = 4,
    [Obsolete("PS3 has been removed in >=5.5")] PS3 = 5,
    [Obsolete("XBOX360 has been removed in 5.5")] XBOX360 = 6,
    Android = 7,
    WebGL = 13, // 0x0000000D
    [Obsolete("Use WSA instead")] Metro = 14, // 0x0000000E
    WSA = 14, // 0x0000000E
    [Obsolete("Use WSA instead")] WP8 = 15, // 0x0000000F
    [Obsolete("BlackBerry has been removed as of 5.4")] BlackBerry = 16, // 0x00000010
    Tizen = 17, // 0x00000011
    PSP2 = 18, // 0x00000012
    PS4 = 19, // 0x00000013
    PSM = 20, // 0x00000014
    XboxOne = 21, // 0x00000015
    [Obsolete("SamsungTV has been removed as of 2017.3")] SamsungTV = 22, // 0x00000016
    N3DS = 23, // 0x00000017
    WiiU = 24, // 0x00000018
    tvOS = 25, // 0x00000019
    Facebook = 26, // 0x0000001A
    Switch = 27, // 0x0000001B
  }
 */