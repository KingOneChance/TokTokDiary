using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Func_InitLoadingScene : MonoBehaviour
{

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene("Main");
    }
}
