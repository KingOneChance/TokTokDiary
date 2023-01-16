using UnityEditor;

public class JenkinsBuild
{
    [MenuItem("JenkinsBuild/Build")]
    static void JenkinsBuildRun()
    {
        //string[] scenes = { "Assets/Scenes/SampleScene.unity" };
        //string curDir = Directory.GetCurrentDirectory() + "\\Build\\"; //본인 프로젝트 경로에 Build란 폴더
        //string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        string[] scenes = UnityEditor.EditorBuildSettingsScene.GetActiveSceneList(UnityEditor.EditorBuildSettings.scenes);
        //BuildPipeline.BuildPlayer(scenes, path + "./mygame.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        BuildPipeline.BuildPlayer(scenes, "Build/TokTokDiary.apk", BuildTarget.Android, BuildOptions.None);
    }
}
