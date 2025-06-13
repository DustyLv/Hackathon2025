#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;

public class ProjectSetup
{
    public static string DefaultCompanyName = "ViAVR";


    [MenuItem("Tools/Create Project Folders")]
    static void CreateProjectFolders()
    {
        string projectFolderName = "_" + GetProjectName();

        AssetDatabase.CreateFolder("Assets", projectFolderName);

        string threeDAssetsGuid = AssetDatabase.CreateFolder("Assets/" + projectFolderName, "3D Assets");
        AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(threeDAssetsGuid), "Materials");
        

        string twoDAssetsGuid = AssetDatabase.CreateFolder("Assets/" + projectFolderName, "2D Assets");
        AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(twoDAssetsGuid), "Sprites & UI");
        AssetDatabase.CreateFolder(AssetDatabase.GUIDToAssetPath(twoDAssetsGuid), "Textures");

        AssetDatabase.CreateFolder("Assets/" + projectFolderName, "Animation");
        AssetDatabase.CreateFolder("Assets/" + projectFolderName, "Audio");
        AssetDatabase.CreateFolder("Assets/" + projectFolderName, "Particles");
        AssetDatabase.CreateFolder("Assets/" + projectFolderName, "Prefabs");
        AssetDatabase.CreateFolder("Assets/" + projectFolderName, "Scenes");
        AssetDatabase.CreateFolder("Assets/" + projectFolderName, "Scripts");
        AssetDatabase.CreateFolder("Assets/" + projectFolderName, "Shaders");
    }

    [MenuItem("Tools/Set Up Product Details")]
    public static void SetupPlayerSettings()
    {
        PlayerSettings.companyName = DefaultCompanyName;
        PlayerSettings.productName = GetProjectName();
    }

    private static string GetProjectName()
    {
        string[] s = Application.dataPath.Split('/');
        string projectName = s[s.Length - 2];
        return projectName;
    }
}

#endif