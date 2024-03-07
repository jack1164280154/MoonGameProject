using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalGames.Util
{
    public static class DebugUtil
    {
        public static void Log(string message)
        {
#if Enable_Debug
            Debug.Log(message);
#endif
        }
        public static void LogError(string message)
        {
#if Enable_Debug
            Debug.LogError(message);
#endif
        }
    }
}

