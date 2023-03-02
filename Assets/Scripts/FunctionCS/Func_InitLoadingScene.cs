using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void Start()
    {
        StartCoroutine(SwingGGamdo());
        StartCoroutine(EmphasizeText());
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
