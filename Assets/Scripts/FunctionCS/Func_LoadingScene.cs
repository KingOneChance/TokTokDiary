using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Func_LoadingScene : MonoBehaviour
{
    static string nextScene;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        SceneManager.UnloadSceneAsync("LoadingScene");
        ao.allowSceneActivation = true;
    }
}
