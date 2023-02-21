using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Func_SwellUpDrag : Func_DragAndDrop
{
    [SerializeField] private GameObject[] SwellUpPanels = null;
    [SerializeField] private Func_SwellUpDrag[] func_SwellUpDrags = null;
    [SerializeField] private Button skipButton = null;
    [SerializeField] private GameObject dummy = null;
    private bool isDragDone = false;
    public bool IsDragDone { get { return isDragDone; } private set { } }

    private new void OnEnable()
    {
        dummy = GameObject.Find("DummyForSound");
        isDragDone = false;
        SwellUpPanels[0].SetActive(true);
        SwellUpPanels[1].SetActive(false);
        skipButton.gameObject.SetActive(false);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (Vector3.Distance(myDestinationPos.position, myRect.position) < 100f)
        {
            Manager_Main.Instance.GetAudio().PlaySound("ComeBack", SoundType.Touch, dummy, false, true);
            isDragDone = true;
            CheckIsDragDone();
        }
    }

    private void CheckIsDragDone()
    {
        for(int i = 0; i < func_SwellUpDrags.Length; ++i)
        {
            if (func_SwellUpDrags[i].IsDragDone == false)
                return;
        }
        SwellUpPanels[0].SetActive(false);
        SwellUpPanels[1].SetActive(true);
        skipButton.gameObject.SetActive(true);
    }
}
