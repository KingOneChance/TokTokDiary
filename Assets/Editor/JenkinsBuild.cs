using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using Debug = UnityEngine.Debug;

public class JenkinsBuild
{
    //[MenuItem("JenkinsBuild/Build")]
    //static void JenkinsBuildRun()
    //{
    //    PlayerSettings.keyaliasPass = "qwerasdf";
    //    PlayerSettings.keystorePass = "qwerasdf";
    //    PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
    //    PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;

    //    //string[] scenes = { "Assets/Scenes/SampleScene.unity" };
    //    //string curDir = Directory.GetCurrentDirectory() + "\\Build\\"; //본인 프로젝트 경로에 Build란 폴더
    //    //string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
    //    string[] scenes = UnityEditor.EditorBuildSettingsScene.GetActiveSceneList(UnityEditor.EditorBuildSettings.scenes);
    //    //BuildPipeline.BuildPlayer(scenes, path + "./mygame.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    //    BuildPipeline.BuildPlayer(scenes, "Build/TokTokDiary.apk", BuildTarget.Android, BuildOptions.None);
    //}

    [MenuItem("JenkinsBuild/Build")]
    public static void JenkinsBuildRun()
    {
        PlayerSettings.Android.useCustomKeystore = true;
        PlayerSettings.Android.keyaliasName = "toktokdiarykey";
        PlayerSettings.Android.keystoreName = "user.keystore";
        PlayerSettings.Android.keystorePass = "qwerasdf";
        PlayerSettings.Android.keyaliasPass = "qwerasdf";


        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = UnityEditor.EditorBuildSettingsScene.GetActiveSceneList(UnityEditor.EditorBuildSettings.scenes);
        buildPlayerOptions.locationPathName = string.Format("Build/톡톡다이어리_{0}.apk", PlayerSettings.bundleVersion);
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
