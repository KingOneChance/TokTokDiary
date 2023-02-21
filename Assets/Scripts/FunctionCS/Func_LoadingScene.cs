using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Func_LoadingScene : MonoBehaviour
{
    [SerializeField] RawImage[] backGroundImgs = null;

    static string nextScene;
    public bool isLoadingDone = true;
    private bool isUnloadingDone = true;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
    }

    private void Start()
    {
        StartCoroutine(UnloadSceneProcess());
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        isLoadingDone = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            yield return new WaitForSeconds(3f);
            SceneManager.UnloadSceneAsync("LoadingScene");
            ao.allowSceneActivation = true;
            isLoadingDone = true;
        }
    }

    private IEnumerator UnloadSceneProcess()
    {
        isUnloadingDone = false;
        AsyncOperation ao = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            ao.allowSceneActivation = true;
            yield return null;
        }
        isUnloadingDone = true;
    }
}
