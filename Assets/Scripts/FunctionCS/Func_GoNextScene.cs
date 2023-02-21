using UnityEngine;
using UnityEngine.SceneManagement;

public class Func_GoNextScene : MonoBehaviour
{
    public void GoNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
    }
}