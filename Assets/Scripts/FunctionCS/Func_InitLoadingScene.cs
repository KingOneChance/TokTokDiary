using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Android;
//using static NativeCamera;
//using static NativeGallery;


public class Func_InitLoadingScene : MonoBehaviour
{
    [SerializeField] private RectTransform ggamdoRect = null;
    [SerializeField] private RectTransform explainText = null;
    [SerializeField] private Button startButton = null;

    private WaitForFixedUpdate fixedDeltaTime = new WaitForFixedUpdate();
    private WaitForSeconds delay = new WaitForSeconds(0.05f);
    private Vector3 angle = new Vector3(0, 0, 1);

    private void Awake()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Main");
        });
    }

    private IEnumerator Start()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(CO_PermissionCheck());
        }
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => Application.isFocused);

        StartCoroutine(SwingGGamdo());
        StartCoroutine(EmphasizeText());
    }

    private IEnumerator CO_PermissionCheck()
    {
        yield return new WaitForEndOfFrame();

        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

        if(NativeCamera.CheckPermission(true) != NativeCamera.Permission.Granted)
        {
            NativeCamera.RequestPermission(true);
        }

        if (NativeGallery.CheckPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image) != NativeGallery.Permission.Granted)
        {
            NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
        }
    }

    private IEnumerator SwingGGamdo()
    {
        while(true)
        {
            while (ggamdoRect.localEulerAngles.z < 359)
            {
                ggamdoRect.localEulerAngles += angle * Time.deltaTime * 25f;
                yield return fixedDeltaTime;
            }

            while (ggamdoRect.localEulerAngles.z > 341)
            {
                ggamdoRect.localEulerAngles -= angle * Time.deltaTime * 25f;
                yield return fixedDeltaTime;
            }
        }
    }

    private IEnumerator EmphasizeText()
    {
        while (true)
        {
            int i = 0;
            while (i < 60)
            {
                if (i < 10 || (20 <= i && i < 30) || (40 <= i && i < 50))
                    explainText.localScale = new Vector2(explainText.localScale.x + 0.03f, explainText.localScale.y + 0.03f);
                else if (10 <= i && i < 20 || (30 <= i && i < 40) || (50 <= i && i < 60))
                    explainText.localScale = new Vector2(explainText.localScale.x - 0.03f, explainText.localScale.y - 0.03f);

                yield return delay;
                i++;
            } 
        }
    }
}
