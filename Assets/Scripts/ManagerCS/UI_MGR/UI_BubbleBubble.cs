using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_BubbleBubble : MonoBehaviour
{
    [SerializeField] Transform[] positions = null;
    [SerializeField] RawImage image = null;
    private int ranNum = 0;
    void Start()
    {
        ranNum = Random.Range(0, positions.Length);
        image.transform.position = positions[ranNum].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

}
