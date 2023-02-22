using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Func_LoadingScene : MonoBehaviour
{
    [SerializeField] RawImage[] backGroundImgs = null;
    [SerializeField] Toggle[] backGroundToggle = null;
    [SerializeField] RawImage fadeOutImg = null;
    
    public static string nextScene;
    public bool isLoadingDone = true;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
    }

    private void Start()
    {
        StartCoroutine(UnloadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
        NextLoadingSceneProcess();
        ao.allowSceneActivation = false;
        //yield return new WaitUntil(() => isLoadingDone == true);
        while (!ao.isDone)
        {
            yield return new WaitForSeconds(3f);
            SceneManager.UnloadSceneAsync("LoadingScene");
            ao.allowSceneActivation = true;
        }
    }

    private IEnumerator UnloadSceneProcess()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            ao.allowSceneActivation = true;
            yield return null;
        }
        StartCoroutine(LoadSceneProcess());
    }

    private void NextLoadingSceneProcess()
    {
        isLoadingDone = false;
        switch (nextScene)
        {
            case "BubbleSticker" :
                backGroundToggle[0].isOn = true;
                StartCoroutine(BubbleStickerProcess());
                break;

            case "BubbleGun":
                backGroundToggle[1].isOn = true;
                StartCoroutine(BubbleGunProcess());
                break;

            case "BubbleBear":
                backGroundToggle[2].isOn = true;
                StartCoroutine(BubbleBearProcess());
                break;

            case "FreeSticker":
                backGroundToggle[3].isOn = true;
                StartCoroutine(FreeStickerProcess());
                break;
        }
        Manager_Main.Instance.PlayBGM(nextScene);
        for (int i = 0; i < backGroundToggle.Length; i++)
        {
            backGroundImgs[i].gameObject.SetActive(backGroundToggle[i].isOn);
        }
    }

    private IEnumerator BubbleStickerProcess()
    {
        //yield return new WaitForSeconds(3f);
        yield return null;
        isLoadingDone = true;
    }

    private IEnumerator BubbleGunProcess()
    {
        //yield return new WaitForSeconds(3f);
        yield return null;
        isLoadingDone = true;
    }

    private IEnumerator BubbleBearProcess()
    {
        //yield return new WaitForSeconds(3f);
        yield return null;
        isLoadingDone = true;
    }

    private IEnumerator FreeStickerProcess()
    {
        //yield return new WaitForSeconds(3f);
        yield return null;
        isLoadingDone = true;
    }
}
