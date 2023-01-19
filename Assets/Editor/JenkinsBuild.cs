using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using Debug = UnityEngine.Debug;

public class JenkinsBuild
{
    [MenuItem("JenkinsBuild/Build")]
    public static void JenkinsBuildRun()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = UnityEditor.EditorBuildSettingsScene.GetActiveSceneList(UnityEditor.EditorBuildSettings.scenes);
        buildPlayerOptions.locationPathName = string.Format("Build/TokTokDiary_{0}.apk", PlayerSettings.bundleVersion);
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.Development;
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build Succeeded: " + summary.totalSize + "bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build Failed");
        }
    }
}

//using System.Collections.Generic;
//using System.IO;
//using UnityEditor;
//using UnityEditor.Build.Reporting;
//using UnityEngine;

//public class JenkinsBuild
//{
//    static string APP_NAME = "TokTokDiary";

//    [MenuItem("JenkinBuild/Build", false, 1)]
//    static void JenkinsBuildRun()
//    {
//        string target_filename = "TokTokDiary" + ".apk";
//        string[] SCENES = EditorBuildSettingsScene.GetActiveSceneList(UnityEditor.EditorBuildSettings.scenes);
//        GenericBuild(SCENES, target_filename, BuildTarget.Android, BuildOptions.None);
//    }
//    //private static string[] FindEnabledEditorScenes()
//    //{
//    //    List<string> EditorScenes = new List<string>();
//    //    foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
//    //    {
//    //        if (!scene.enabled) continue;
//    //        EditorScenes.Add(scene.path);
//    //    }
//    //    return EditorScenes.ToArray();
//    //}

//    static void GenericBuild(string[] scenes, string target_filename, BuildTarget build_target, BuildOptions build_options)
//    {
//        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
//            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
//        FileInfo info = new FileInfo(Directory.GetCurrentDirectory() + "/Build");

//        if(info.Exists == false)
//        {
//            Directory.CreateDirectory(info.FullName);
//        }
//    }
//}
