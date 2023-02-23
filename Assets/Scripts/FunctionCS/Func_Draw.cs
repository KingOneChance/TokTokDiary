using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FreeDraw
{
    public class Func_Draw : MonoBehaviour
    {
        [Header("===Frame shape check===")]
        [SerializeField] protected bool isCircleFrame = false;
        [SerializeField] protected float circleSize;
        [SerializeField] public Slider Pen_Width;
        [SerializeField] public LineRenderer line = null;
        [SerializeField] protected GameObject linePrefab;
        [SerializeField] protected GameObject obj;
        [SerializeField] EdgeCollider2D col;
        [SerializeField] protected float drawingAreaMaxX = 0f;
        [SerializeField] protected float drawingAreaMinX = 0f;
        [SerializeField] protected float drawingAreaMaxY = 0f;
        [SerializeField] protected float drawingAreaMinY = 0f;
        [SerializeField] protected float writingAreaMaxX = 0f;
        [SerializeField] protected float writingAreaMinX = 0f;
        [SerializeField] protected float writingAreaMaxY = 0f;
        [SerializeField] protected float writingAreaMinY = 0f;
        [SerializeField] protected float titleAreaMaxX = 0f;
        [SerializeField] protected float titleAreaMinX = 0f;
        [SerializeField] protected float titleAreaMaxY = 0f;
        [SerializeField] protected float titleAreaMinY = 0f;
        [SerializeField] protected Camera mainCam = null;

        [Header("===InitialClickPoisitionCheck===")]
        [SerializeField] protected bool internalClick = false;
        [SerializeField] protected bool isDragSticker = false;
        [SerializeField] protected bool isDiaryScene = false;
        [SerializeField] protected bool onObject;
        List<Vector2> points = new List<Vector2>();
        public Color curColor = Color.black;
        private GameObject tempOBJ;
        protected float currentPenWidth = 0f;
        protected float fixedPenWidth = 0.3f;

        //    const float Min_Pen_Width = 0.1f;
        //    const float Max_Pen_Width = 1.0f;
        public void StopDraw(bool isStop)
        {
            isDragSticker = isStop;
            if (isStop)
                onObject = true;
            else
                onObject = false;
        }

        protected void Awake()
        {
            mainCam = Camera.main;
            line = GetComponent<LineRenderer>();
        }

        protected void Start()
        {
            currentPenWidth = 0.3f;
        }

        protected void Update()
        {
            /*   if (onObject == false)
               {
                   if (CheckArea() == true)
                   {
                       Draw();
                   }
                   else internalClick = false;
               }*/
        }

        protected void Draw()
        {
            //영역에서 버튼 눌렸을 때, 외부 영역에서 클릭하고 드래그해서 영역안에 왔을 때,
            if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && (internalClick == false || onObject == true)))
            {
                SData_NodeData temp = new SData_NodeData();
                internalClick = true;
                obj = Instantiate(linePrefab);
                obj.tag = "Line";
                tempOBJ = obj;
                line = obj.GetComponent<LineRenderer>();
                col = obj.GetComponent<EdgeCollider2D>();
                obj.transform.position = Vector3.zero;
                line.startColor = curColor;
                line.endColor = curColor;
                line.startWidth = currentPenWidth;
                line.endWidth = currentPenWidth;
                line.positionCount = 1;
                points.Add(mainCam.ScreenToWorldPoint(Input.mousePosition));
                line.SetPosition(0, points[0]);


                temp.position = obj.transform.position;
                temp.rotation = obj.transform.rotation.eulerAngles;
                temp.scale = obj.transform.localScale;
                if (isDiaryScene == true)
                    Manager_Main.Instance.manager_PictureDiary.AddDragInit(temp, obj);
            }
            //during drag
            else if (Input.GetMouseButton(0) && internalClick == true)
            {
                Manager_Main.Instance.GetAudio().PlaySound("Mop", SoundType.Diary, gameObject, false, false);
                Vector2 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                /*     if (Vector2.Distance(pos, points[points.Count - 1]) < 0.001f)
                     {
                         return;
                     }*/
                points.Add(pos);
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, pos);
                col.points = points.ToArray();
            }
            //end drag
            else if (Input.GetMouseButtonUp(0))
            {
                Manager_Main.Instance.GetAudio().StopPlaySound(gameObject);
                if (isDragSticker == true) Destroy(tempOBJ);
                points.Clear();
            }
        }


        public void MMMMMM()
        {
            onObject = true;
        }
        public void NNNNN()
        {
            onObject = false;
        }

        protected bool CheckArea()
        {
            if (isCircleFrame == false)
            {
                Vector3 curTouchPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

                if (curTouchPos.x < drawingAreaMaxX && curTouchPos.y < drawingAreaMaxY &&
                    curTouchPos.x > drawingAreaMinX && curTouchPos.y > drawingAreaMinY ||
                    curTouchPos.x < writingAreaMaxX && curTouchPos.y < writingAreaMaxY &&
                    curTouchPos.x > writingAreaMinX && curTouchPos.y > writingAreaMinY ||
                    curTouchPos.x < titleAreaMaxX && curTouchPos.y < titleAreaMaxY &&
                    curTouchPos.x > titleAreaMinX && curTouchPos.y > titleAreaMinY)
                {
                    if (curTouchPos.x < writingAreaMaxX && curTouchPos.y < writingAreaMaxY &&
                        curTouchPos.x > writingAreaMinX && curTouchPos.y > writingAreaMinY ||
                        curTouchPos.x < titleAreaMaxX && curTouchPos.y < titleAreaMaxY &&
                        curTouchPos.x > titleAreaMinX && curTouchPos.y > titleAreaMinY)
                    {
                        currentPenWidth = 0.05f;
                        // 기존에 쓰는 지우개 함수를 쓰기전용 지우개 함수로 교체한다.
                    }

                    else
                    {
                        currentPenWidth = fixedPenWidth;
                    }

                    return true;
                }
                else
                {
                    points.Clear();
                    return false;
                }
            }
            else
            {
                Vector3 curTouchPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                // this condition for draw only circle frame, so Use this equation of a circle 
                if (curTouchPos.x * curTouchPos.x + curTouchPos.y * curTouchPos.y <= circleSize * circleSize)
                    return true;
                else
                {
                    points.Clear();
                    return false;
                }
            }
        }
        public void selectedColor(ColorType type)
        {
            switch (type)
            {
                case ColorType.Red:
                    curColor = new Color32(247, 67, 67, 255);
                    break;
                case ColorType.Orange:
                    curColor = new Color32(248, 151, 49, 255);
                    break;
                case ColorType.Yellow:
                    curColor = new Color32(253, 241, 100, 255);
                    break;
                case ColorType.Green:
                    curColor = new Color32(147, 202, 91, 255);
                    break;
                case ColorType.Blue:
                    curColor = new Color32(98, 150, 207, 255);
                    break;
                case ColorType.Purple:
                    curColor = new Color32(158, 127, 186, 255);
                    break;
                case ColorType.Pink:
                    curColor = new Color32(233, 160, 198, 255);
                    break;
                case ColorType.Brown:
                    curColor = new Color32(141, 107, 75, 255);
                    break;
                case ColorType.Black:
                    curColor = new Color32(30, 30, 30, 255);
                    break;
                case ColorType.Eraser:
                    curColor = new Color32(255, 255, 255, 255);
                    break;
                case ColorType.WritingEraser:
                    curColor = new Color32(181, 203, 203, 255);
                    break;
            }
        }

        protected void SetFixedPenWidth(float width)
        {
            fixedPenWidth = width;
        }


        public void ControllPenWidth()
        {
            currentPenWidth = Pen_Width.value;
        }
    }
}
