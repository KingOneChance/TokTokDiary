using UnityEngine;
using UnityEngine.SceneManagement;

public class Func_GoNextScene : MonoBehaviour
{
    public void GoNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
        Manager_Main.Instance.PlayBGM(sceneName);
    }

    public void GoNextSceneThroughLoadingScene(string sceneName)
    {
        Func_LoadingScene.LoadScene(sceneName);
    }
}