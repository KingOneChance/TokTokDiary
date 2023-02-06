using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FreeDraw
{
    public class Func_Draw : MonoBehaviour
    {
        [Header("===Frame shape check===")]
        [SerializeField] private bool isCircleFrame = false;
        [SerializeField] private float circleSize;

        [SerializeField] GameObject linePrefab;
        [SerializeField] public Slider Pen_Width;
        [SerializeField] public LineRenderer line = null;
        public Color curColor = Color.black;
        [SerializeField] EdgeCollider2D col;
        List<Vector2> points = new List<Vector2>();

        [SerializeField] private float drawingAreaMaxX = 0f;
        [SerializeField] private float drawingAreaMinX = 0f;
        [SerializeField] private float drawingAreaMaxY = 0f;
        [SerializeField] private float drawingAreaMinY = 0f;
        [SerializeField] private Camera mainCam = null; 

        [Header("===InitialClickPoisitionCheck===")]
        [SerializeField] private bool internalClick = false;

        [SerializeField] private bool isDragSticker = false;
        private GameObject tempOBJ;
        [SerializeField] private bool onObject;

        private float currentPenWidth = 0f;

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
        
     //   private void Start()
     //   {
     //       Pen_Width.onValueChanged.AddListener(delegate { Pen_WidthTest(); });
     //   }
     //
     //   public void Pen_WidthTest()
     //   {
     //       float diff = Max_Pen_Width - Min_Pen_Width;
     //       float value = Min_Pen_Width + (diff + Pen_Width.value);
     //   }
        private void Awake()
        {
            mainCam = Camera.main;
            line = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            currentPenWidth = 0.3f;
        }

        private void Update()
        {
            if (onObject == false)
            {
                if (CheckArea() == true)
                {
                    //영역에서 버튼 눌렸을 때, 외부 영역에서 클릭하고 드래그해서 영역안에 왔을 때,
                    if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && (internalClick == false || onObject == true)))
                    {
                        internalClick = true;
                        GameObject Obj = Instantiate(linePrefab);
                        tempOBJ = Obj;
                        line = Obj.GetComponent<LineRenderer>();
                        col = Obj.GetComponent<EdgeCollider2D>();
                        Obj.transform.position = Vector3.zero;
                        line.startColor = curColor;
                        line.endColor = curColor;
                        line.startWidth = currentPenWidth;
                        line.endWidth = currentPenWidth;
                        line.positionCount = 1;
                        points.Add(mainCam.ScreenToWorldPoint(Input.mousePosition));
                        line.SetPosition(0, points[0]);
                    }
                    //during drag
                    else if (Input.GetMouseButton(0) && internalClick == true)
                    {
                        Vector2 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                        if (Vector2.Distance(pos, points[points.Count - 1]) < 0.001f)
                        {
                            return;
                        }
                        points.Add(pos);
                        line.positionCount++;
                        line.SetPosition(line.positionCount - 1, pos);
                        col.points = points.ToArray();
                    }
                    //end drag
                    else if (Input.GetMouseButtonUp(0))
                    {
                        points.Clear();
                        if (isDragSticker == true) Destroy(tempOBJ);
                    }
                }
                else internalClick = false;
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

      

        public bool CheckArea()
        {
            if (isCircleFrame == false)
            {
                Vector3 curTouchPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

                if (curTouchPos.x < drawingAreaMaxX && curTouchPos.y < drawingAreaMaxY &&
                    curTouchPos.x > drawingAreaMinX && curTouchPos.y > drawingAreaMinY)
                {
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
            }
        }

        public void ControllPenWidth()
        {
            currentPenWidth = Pen_Width.value;
        }
    }
}
