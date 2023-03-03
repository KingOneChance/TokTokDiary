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
        buildPlayerOptions.scenes = EditorBuildSettingsScene.GetActiveSceneList(UnityEditor.EditorBuildSettings.scenes);
        buildPlayerOptions.locationPathName = string.Format("TokTokBuild/TokTokDiary_{0}.apk", PlayerSettings.bundleVersion);
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.Development;
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
    }
}
