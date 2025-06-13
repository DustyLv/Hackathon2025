using UnityEditor;
using UnityEngine;

public class AppVersionControl : EditorWindow
{
    private static int major, minor, patch, bundleVersionCode;
    private static string bundleVersion;

    [MenuItem("Tools/App Version Control")]
    public static void ShowWindow()
    {
        GetWindow<AppVersionControl>("App Version Control");
        LoadVersion();
    }

    private void OnGUI()
    {
        GUILayout.Label("Current Version:", EditorStyles.boldLabel);
        GUILayout.Label($"Semantic Version: {major}.{minor}.{patch}");
        GUILayout.Label($"Bundle Version: {bundleVersion} (Code: {bundleVersionCode})");

        GUILayout.Space(10);

        if (GUILayout.Button("Increment Major"))
        {
            major++;
            minor = 0;
            patch = 0;
            IncrementBundleVersion();
        }

        if (GUILayout.Button("Increment Minor"))
        {
            minor++;
            patch = 0;
            IncrementBundleVersion();
        }

        if (GUILayout.Button("Increment Patch"))
        {
            patch++;
            IncrementBundleVersion();
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Save Version"))
        {
            SaveVersion();
        }
    }

    private static void LoadVersion()
    {
        string version = PlayerSettings.bundleVersion;
        bundleVersionCode = PlayerSettings.Android.bundleVersionCode;

        string[] versionParts = version.Split('.');
        if (versionParts.Length >= 3)
        {
            int.TryParse(versionParts[0], out major);
            int.TryParse(versionParts[1], out minor);
            int.TryParse(versionParts[2], out patch);
        }
        else
        {
            major = minor = patch = 0;
        }

        bundleVersion = version;
    }

    private static void IncrementBundleVersion()
    {
        bundleVersionCode++;
        bundleVersion = $"{major}.{minor}.{patch}";
        SaveVersion();
    }

    private static void SaveVersion()
    {
        PlayerSettings.bundleVersion = bundleVersion;
        PlayerSettings.Android.bundleVersionCode = bundleVersionCode;
        // EditorUtility.SetDirty(PlayerSettings.GetSerializedObject().targetObject);
        AssetDatabase.SaveAssets();
        Debug.Log($"Updated Version: {bundleVersion} (Code: {bundleVersionCode})");
    }
}
