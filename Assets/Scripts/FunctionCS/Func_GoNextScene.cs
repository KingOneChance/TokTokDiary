using UnityEngine;
using UnityEngine.SceneManagement;

public class Func_GoNextScene : MonoBehaviour
{
    public void GoNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoNextSceneThroughLoadingScene(string sceneName)
    {

    }
}
