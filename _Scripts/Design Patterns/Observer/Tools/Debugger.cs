using DesignPatterns;
using UnityEngine;

namespace Tools
{
    public class Debugger : Singleton<Debugger>
    {
        public void DebugValue(int value)
        {
            Debug.Log($"Value changed to {value}");
        }
    }
}
