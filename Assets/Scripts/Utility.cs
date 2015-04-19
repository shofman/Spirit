using UnityEditor;
using System.Reflection;
using System;

/**
 * Utility class for housing universal unity functions
 */
public class Utility {
    /**
     * Function to remove the log from the main development page
     */
    public static void clearLog() {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type type = assembly.GetType("UnityEditorInternal.LogEntries");
        MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}