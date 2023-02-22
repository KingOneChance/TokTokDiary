using UnityEngine;
using UnityEngine.EventSystems;

public class Func_DragAndInstantiate : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] GameObject myObj = null;

    private Transform StickerTr = null;
    private CanvasGroup myCanvasGroup = null;
    private Vector3 myInitPos = Vector3.zero;
    private RectTransform rect = null;

    public int myidx = 0;

    private void Awake()
    {
        myidx = 0;
        StickerTr = GameObject.Find("Stickers").transform;
        myCanvasGroup = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
        myInitPos = rect.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        myCanvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        myCanvasGroup.blocksRaycasts = true;
        if (myidx == 0)
        {
            SData_NodeData temp = new SData_NodeData();
            RectTransform tempRect = new RectTransform();
            GameObject go = Instantiate(myObj, eventData.position, Quaternion.identity);
            go.transform.SetParent(StickerTr);
            go.transform.SetAsLastSibling();
            Destroy(go.GetComponent<Func_DragAndInstantiate>());
          
            go.AddComponent<Func_DragObject>();
            tempRect = go.GetComponent<RectTransform>();
            temp.position = tempRect.position;
            temp.rotation = tempRect.rotation.eulerAngles;
            temp.scale = tempRect.localScale;
            Manager_Main.Instance.manager_PictureDiary.AddDragInit(temp, go);

            rect.transform.position = myInitPos;
        }
    }
}
