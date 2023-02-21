using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Func_LoadingScene : MonoBehaviour
{
    [SerializeField] RawImage[] backGroundImgs = null;
    static string nextScene;
    public bool isLoadingDone = true;

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
        isLoadingDone = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        Debug.Log("1");
        ao.allowSceneActivation = false;
        Debug.Log("2");

        while (!ao.isDone)
        {
            Debug.Log("3");

            yield return new WaitForSeconds(3f);
            Debug.Log("4");

            SceneManager.UnloadSceneAsync("LoadingScene");
            Debug.Log("5");

            ao.allowSceneActivation = true;
            Debug.Log("6");
            isLoadingDone = true;
            yield return null;
        }
    }
}
