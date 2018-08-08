using System.Collections;

namespace SKTools.ScriptingDefineManager
{
    public interface IGroup
    {
        string Id { get; }
        bool Active { get; set; }
        IList Defines { get; }
        IGroup Clone();
    }
}