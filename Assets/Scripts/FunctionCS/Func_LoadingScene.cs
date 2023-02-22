using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Func_LoadingScene : MonoBehaviour
{
    [SerializeField] RawImage[] backGroundImgs = null;
    [SerializeField] Toggle[] backGroundToggle = null;
    [SerializeField] RawImage fadeOutImg = null;
    [SerializeField] RawImage[] bubblBearNoteImgs = null;
    [SerializeField] RectTransform bathkkamdo = null;
    [SerializeField] GameObject[] freeStickerBubbles = null;

    public static string nextScene;
    public bool isLoadingDone = true;

    private WaitForSeconds bubbleBearNoteOnDelay = new WaitForSeconds(0.375f);
    private WaitForFixedUpdate bathRotatedeltaTime = new WaitForFixedUpdate();
    private Vector3 angle = new Vector3(0, 0, 1);

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
    }

    private void Start()
    {
        StartCoroutine(UnloadSceneProcess());
        Manager_Main.Instance.PlayBGM(nextScene);
    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
        NextLoadingSceneProcess();
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            yield return new WaitUntil(() => isLoadingDone == true);
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

        for (int i = 0; i < backGroundToggle.Length; i++)
        {
            backGroundImgs[i].gameObject.SetActive(backGroundToggle[i].isOn);
        }
    }

    private IEnumerator BubbleStickerProcess()
    {
        yield return new WaitForSeconds(3f);
        isLoadingDone = true;
    }

    private IEnumerator BubbleGunProcess()
    {
        for (int i = 0; i < 3; i++)
        {
            while (bathkkamdo.localEulerAngles.z < 359)
            {
                bathkkamdo.localEulerAngles += angle * Time.deltaTime * 25f;
                yield return bathRotatedeltaTime;
            }

            while (bathkkamdo.localEulerAngles.z > 341)
            {
                Debug.Log(bathkkamdo.rotation.z);
                bathkkamdo.localEulerAngles -= angle * Time.deltaTime * 25f;
                yield return bathRotatedeltaTime;
            } 
        }
        isLoadingDone = true;
    }

    private IEnumerator BubbleBearProcess()
    {
        for(int i = 0; i < bubblBearNoteImgs.Length; i++)
        {
            bubblBearNoteImgs[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < bubblBearNoteImgs.Length; i++)
        {
            bubblBearNoteImgs[i].gameObject.SetActive(true);
            yield return bubbleBearNoteOnDelay;
        }
        isLoadingDone = true;
    }

    private IEnumerator FreeStickerProcess()
    {
        int runCount = 0;
        while (runCount < 10)
        {
            for (int i = 0; i < freeStickerBubbles.Length; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    freeStickerBubbles[i].transform.GetChild(i).gameObject.SetActive(true);
                }
                yield return new WaitForSeconds(0.3f);
                runCount++;
            }
        }
        isLoadingDone = true;
    }
}
