using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeDraw
{
    public class Func_Draw : MonoBehaviour
    {
        [Header("===Frame shape check===")]
        [SerializeField] private bool isCircleFrame = false;
        [SerializeField] private float circleSize;

        [SerializeField] GameObject linePrefab;
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
        [SerializeField] private bool externalClick = false;

        private bool isDragSticker = false;

        public void StopDraw(bool isStop)
        {
            isDragSticker = isStop;
        }

        private void Awake()
        {
            mainCam = Camera.main;
            line = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (CheckArea() == true && isDragSticker == false)
            {

                if (Input.GetMouseButtonDown(0)||((Input.GetMouseButton(0) && externalClick == false)))
                {
                    externalClick = true;
                    GameObject Obj = Instantiate(linePrefab);
                    line = Obj.GetComponent<LineRenderer>();
                    col = Obj.GetComponent<EdgeCollider2D>();
                    Obj.transform.position = Vector3.zero;
                    Obj.GetComponent<LineRenderer>().startColor = curColor;
                    Obj.GetComponent<LineRenderer>().endColor = curColor;

                    points.Add(mainCam.ScreenToWorldPoint(Input.mousePosition));
                    line.positionCount = 1;
                    line.SetPosition(0, points[0]);
                }
                else if (Input.GetMouseButton(0) && externalClick == true)
                {
                    Vector2 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                    points.Add(pos);
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, pos);
                    col.points = points.ToArray();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    points.Clear();
                }
            }
            else externalClick = false;
        }

        private bool CheckArea()
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
                    curColor = new Color32(158, 157, 186, 255);
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
    }
}
