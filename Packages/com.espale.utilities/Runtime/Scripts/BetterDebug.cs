using UnityEngine;

namespace Espale.Utilities
{
    /// <summary>
    /// Provides wrappers for common debug tools such as Debug.Log, Debug.LogWarning and Debug.LogError.
    /// These methods are wrapped around preprocessor if statements to make sure they only run in-editor
    /// which helps with performance. Note that if you wish to see these debug calls in builds, use the regular
    /// debug methods.
    /// </summary>
    public static class BetterDebug
    {
        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }
        public static void Log(object message, Object context)
        {
#if UNITY_EDITOR
            Debug.Log(message, context);
#endif
        }
        
        public static void LogWarning(object message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
        }
        public static void LogWarning(object message, Object context)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message, context);
#endif
        }
        
        public static void LogError(object message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
#endif
        }
        public static void LogError(object message, Object context)
        {
#if UNITY_EDITOR
            Debug.LogError(message, context);
#endif
        }
    }
}
