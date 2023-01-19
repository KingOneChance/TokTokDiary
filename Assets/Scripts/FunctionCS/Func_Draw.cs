using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeDraw
{
    public class Func_Draw : MonoBehaviour
    {
        [SerializeField] GameObject linePrefab;
        [SerializeField] public LineRenderer line = null;
        public Color curColor = Color.black;
        [SerializeField] EdgeCollider2D col;
        List<Vector2> points = new List<Vector2>();

        [SerializeField] float drawingAreaMaxX = 0f;
        [SerializeField] float drawingAreaMinX = 0f;
        [SerializeField] float drawingAreaMaxY = 0f;
        [SerializeField] float drawingAreaMinY = 0f;

        [SerializeField] Camera mainCam = null;

        private void Awake()
        {
            mainCam = Camera.main;
            line = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (CheckArea() == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
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

                else if (Input.GetMouseButton(0))
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
        }

        private bool CheckArea()
        {
            Vector3 curTouchPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            if (curTouchPos.x < drawingAreaMaxX && curTouchPos.y < drawingAreaMaxY &&
                curTouchPos.x > drawingAreaMinX && curTouchPos.y > drawingAreaMinY)
            {

                return true;
            }
            else
                return false;
        }

        public void selectedColor(ColorType type)
        {
            switch (type)
            {
                case ColorType.Red :
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
    }
}
