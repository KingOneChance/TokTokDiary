using System.Diagnostics;
using UnityEditor;
using UnityEditor.Build.Reporting;

//public class JenkinsBuild
//{
//    //[MenuItem("JenkinsBuild/Build")]
//    //static void JenkinsBuildRun()
//    //{
//    //    PlayerSettings.keyaliasPass = "qwerasdf";
//    //    PlayerSettings.keystorePass = "qwerasdf";
//    //    PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
//    //    PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;

//    //    //string[] scenes = { "Assets/Scenes/SampleScene.unity" };
//    //    //string curDir = Directory.GetCurrentDirectory() + "\\Build\\"; //본인 프로젝트 경로에 Build란 폴더
//    //    //string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
//    //    string[] scenes = UnityEditor.EditorBuildSettingsScene.GetActiveSceneList(UnityEditor.EditorBuildSettings.scenes);
//    //    //BuildPipeline.BuildPlayer(scenes, path + "./mygame.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
//    //    BuildPipeline.BuildPlayer(scenes, "Build/TokTokDiary.apk", BuildTarget.Android, BuildOptions.None);
//    //}

//    [MenuItem("JenkinsBuild/Build")]
//    public static void JenkinsBuildRun()
//    {
//        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
//        buildPlayerOptions.scenes = UnityEditor.EditorBuildSettingsScene.GetActiveSceneList(UnityEditor.EditorBuildSettings.scenes);
//        buildPlayerOptions.locationPathName = string.Format("Build(AOS)/톡톡다이어리_{0}.apk", PlayerSettings.bundleVersion);
//        buildPlayerOptions.target = BuildTarget.Android;
//        buildPlayerOptions.options = BuildOptions.Development;
//        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
//        BuildSummary summary = report.summary;

//        if(summary.result == BuildResult.Succeeded)
//        {
//            Debug.Print("Build Succeeded: " + summary.totalSize + "bytes");
//        }

//        if (summary.result == BuildResult.Failed)
//        {
//            Debug.Print("Build Failed");
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.IO;
using System;

public class JenkinsBuild
{
    static string[] SCENES = FindEnabledEditorScenes();
    static string APP_NAME = "톡톡다이어리";

    [UnityEditor.MenuItem("JenkinsBuild/Build", false, 1)]
    static void JenkinsBuildRun()
    {
        //if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Build/"))
        //{
        //    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Build/");
        //}

        string target_filename = "Build/" + APP_NAME + ".apk";
        SCENES = FindEnabledEditorScenes();

        GenericBuild(SCENES, target_filename, BuildTarget.Android, BuildOptions.None);
    }
    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_filename, BuildTarget build_target, BuildOptions build_options)
    {
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        BuildPipeline.BuildPlayer(scenes, Directory.GetCurrentDirectory() + "/Build/" + target_filename, build_target, build_options);
    }
}
